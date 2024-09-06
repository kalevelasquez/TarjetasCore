import React, { useState, useEffect } from 'react';
import { getHistorialPagos, crearPago } from '../data/data';
import { Transacciones, Pago, GenericResponse } from '../data/definitions';
import * as XLSX from 'xlsx';
import jsPDF from 'jspdf';
import 'jspdf-autotable';

interface HistorialPagosProps {
  numeroTarjeta: string;
}

const formatDate = (date: Date) => {
  const day = String(date.getDate()).padStart(2, '0');
  const month = String(date.getMonth() + 1).padStart(2, '0');
  const year = date.getFullYear();
  return `${day}-${month}-${year}`;
};

const HistorialPagos: React.FC<HistorialPagosProps> = ({ numeroTarjeta }) => {
  const [pagos, setPagos] = useState<Transacciones[]>([]);
  const [mesSeleccionado, setMesSeleccionado] = useState<number>(new Date().getMonth() + 1);
  const [busqueda, setBusqueda] = useState<string>('');
  const [pagosFiltrados, setPagosFiltrados] = useState<Transacciones[]>([]);
  const [modalVisible, setModalVisible] = useState<boolean>(false);
  const [nuevoPago, setNuevoPago] = useState<Pago>({
    numeroTarjeta,
    fechaPago: new Date(),
    descripcion: '',
    montoPago: 0
  });

  useEffect(() => {
    async function fetchPagos() {
      try {
        const currentYear = new Date().getFullYear();
        const data = await getHistorialPagos(numeroTarjeta, mesSeleccionado, currentYear);
        if (data && data.item) {
          setPagos(data.item);
          setPagosFiltrados(data.item);  
        } else {
          console.error('Error al obtener los pagos:', data);
        }
      } catch (error) {
        console.error('Error al obtener los pagos:', error);
      }
    }

    fetchPagos();
  }, [mesSeleccionado, numeroTarjeta]);

  // Manejo de cambio de mes en el select
  const handleMesChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    setMesSeleccionado(parseInt(event.target.value));
    setBusqueda('');  
  };

  // Filtrar pagos por descripción
  useEffect(() => {
    setPagosFiltrados(
      pagos.filter(pago =>
        pago.descripcion.toLowerCase().includes(busqueda.toLowerCase())
      )
    );
  }, [busqueda, pagos]);

  // Función para exportar a XLSX
  const exportToXLSX = () => {
    if (pagosFiltrados.length === 0) return;

    const ws = XLSX.utils.json_to_sheet(pagosFiltrados.map(pago => ({
      Fecha: formatDate(new Date(pago.fechaTransaccion)),
      Descripción: pago.descripcion,
      Monto: `$${pago.monto.toFixed(2)}`,
    })));

    const wb = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Pagos');
    XLSX.writeFile(wb, 'pagos.xlsx');
  };

  // Función para exportar a PDF
  const exportToPDF = () => {
    if (pagosFiltrados.length === 0) return;

    const doc = new jsPDF();
    doc.text('Detalle de pagos realizados', 14, 16);
    
    const tableData = pagosFiltrados.map(pago => [
      formatDate(new Date(pago.fechaTransaccion)),
      pago.descripcion,
      `$${pago.monto.toFixed(2)}`,
    ]);

    doc.autoTable({
      startY: 24,
      head: [['Fecha', 'Descripción', 'Monto']],
      body: tableData,
      styles: { overflow: 'linebreak' },
      columnStyles: {
        2: { halign: 'right' }
      },
    });

    doc.save('pagos.pdf');
  };

  // Manejo del formulario del modal
  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setNuevoPago(prevState => ({
      ...prevState,
      [name]: name === 'montoPago' ? parseFloat(value) : value
    }));
  };

  const handleSave = async () => {
    if (nuevoPago.descripcion === '' || nuevoPago.montoPago <= 0) {
      alert('Por favor, completa todos los campos.');
      return;
    }
  
    // Formatear la fecha al formato requerido por la API
    const fechaFormateada = nuevoPago.fechaPago.toISOString();
  
    const pagoAEnviar = {
      numeroTarjeta: nuevoPago.numeroTarjeta,
      fechaPago: fechaFormateada,
      descripcion: nuevoPago.descripcion,
      montoPago: nuevoPago.montoPago
    };
  
    try {
      const response: GenericResponse = await crearPago(pagoAEnviar);
      if (response.code == 1) {
        alert('Pago agregado exitosamente.');
        setModalVisible(false);
        // Actualiza la lista de pagos
        const currentYear = new Date().getFullYear();
        const data = await getHistorialPagos(numeroTarjeta, mesSeleccionado, currentYear);
        if (data && data.item) {
          setPagos(data.item);
          setPagosFiltrados(data.item);
        }
      } else {
        alert('Error al agregar el pago.');
      }
    } catch (error) {
      console.error('Error al agregar el pago:', error);
      alert('Error al agregar el pago.');
    }
  };

  const handleModalOpen = () => {
    setModalVisible(true);
  };

  const handleModalClose = () => {
    setModalVisible(false);
    // Limpia el formulario al cerrar
    setNuevoPago({
      numeroTarjeta,
      fechaPago: new Date(),
      descripcion: '',
      montoPago: 0
    });
  };

  return (
    <div style={{ padding: '10px', maxWidth: '100%' }}>
      <h3 style={{ textAlign: 'center', marginBottom: '20px', fontWeight: 'bold', fontSize: '1.5rem' }}>Detalle de pagos realizados</h3>

      <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: '20px' }}>
        <input
          type="text"
          placeholder="Buscar pago..."
          value={busqueda}
          onChange={(e) => setBusqueda(e.target.value)}
          style={{
            borderRadius: '20px',
            padding: '10px',
            border: '1px solid #ccc',
            width: '40%',
          }}
        />

        <select
          value={mesSeleccionado}
          onChange={handleMesChange}
          style={{
            borderRadius: '20px',
            padding: '10px',
            border: '1px solid #ccc',
            width: '30%',
          }}
        >
          {[...Array(12)].map((_, index) => (
            <option key={index} value={index + 1}>
              {new Date(0, index).toLocaleString('default', { month: 'long' })}
            </option>
          ))}
        </select>

        <div style={{ display: 'flex', gap: '10px' }}>
          <button
            onClick={exportToXLSX}
            disabled={pagosFiltrados.length === 0}
            style={{
              backgroundColor: '#1b401e',
              color: '#fff',
              border: 'none',
              borderRadius: '10px',
              padding: '10px 20px',
              cursor: 'pointer',
            }}
          >
            XLSX
          </button>

          <button
            onClick={exportToPDF}
            disabled={pagosFiltrados.length === 0}
            style={{
              backgroundColor: '#660c07',
              color: '#fff',
              border: 'none',
              borderRadius: '10px',
              padding: '10px 20px',
              cursor: 'pointer',
            }}
          >
            PDF
          </button>

          <button
            onClick={handleModalOpen}
            style={{
              backgroundColor: '#007bff',
              color: '#fff',
              border: 'none',
              borderRadius: '10px',
              padding: '10px 20px',
              cursor: 'pointer',
            }}
          >
            Agregar nuevo pago
          </button>
        </div>
      </div>

      <table style={{ width: '100%', borderCollapse: 'collapse' }}>
        <thead>
          <tr>
            <th style={{ borderBottom: '1px solid #ccc', padding: '10px' }}>Fecha</th>
            <th style={{ borderBottom: '1px solid #ccc', padding: '10px' }}>Descripción</th>
            <th style={{ borderBottom: '1px solid #ccc', padding: '10px' }}>Monto</th>
          </tr>
        </thead>
        <tbody>
          {pagosFiltrados.length > 0 ? (
            pagosFiltrados.map(pago => (
              <tr key={pago.fechaTransaccion}>
                <td style={{ padding: '10px' }}>{formatDate(new Date(pago.fechaTransaccion))}</td>
                <td style={{ padding: '10px' }}>{pago.descripcion}</td>
                <td style={{ padding: '10px', color: pago.tipoTransact === 'P' ? 'green' : 'red' }}>
                  ${pago.monto.toFixed(2)}
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan={3} style={{ padding: '10px', textAlign: 'center' }}>No hay pagos para mostrar.</td>
            </tr>
          )}
        </tbody>
      </table>

      {modalVisible && (
        <div style={{
          position: 'fixed',
          top: '0',
          left: '0',
          width: '100%',
          height: '100%',
          backgroundColor: 'rgba(0,0,0,0.5)',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
        }}>
          <div style={{
            backgroundColor: '#fff',
            padding: '20px',
            borderRadius: '10px',
            width: '400px',
            boxShadow: '0 4px 8px rgba(0,0,0,0.2)'
          }}>
            <h3 style={{ textAlign: 'center', marginBottom: '20px' }}>Agregar nuevo pago</h3>
            <label>Descripción:</label>
            <input
              type="text"
              name="descripcion"
              value={nuevoPago.descripcion}
              onChange={handleInputChange}
              style={{ width: '100%', padding: '10px', marginBottom: '10px', borderRadius: '5px', border: '1px solid #ccc' }}
            />
            <label>Monto:</label>
            <input
              type="number"
              name="montoPago"
              value={nuevoPago.montoPago}
              onChange={handleInputChange}
              style={{ width: '100%', padding: '10px', marginBottom: '10px', borderRadius: '5px', border: '1px solid #ccc' }}
            />
            <div style={{ textAlign: 'right' }}>
              <button
                onClick={handleModalClose}
                style={{ backgroundColor: '#ccc', color: '#000', border: 'none', borderRadius: '5px', padding: '10px', marginRight: '10px' }}
              >
                Cancelar
              </button>
              <button
                onClick={handleSave}
                style={{ backgroundColor: '#007bff', color: '#fff', border: 'none', borderRadius: '5px', padding: '10px' }}
              >
                Guardar
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default HistorialPagos;
