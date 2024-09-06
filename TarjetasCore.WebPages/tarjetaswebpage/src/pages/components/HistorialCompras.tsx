import React, { useState, useEffect } from 'react';
import { getHistorialCompras, crearCompra } from '../data/data';
import { Transacciones, Compra, GenericResponse } from '../data/definitions';
import * as XLSX from 'xlsx';
import jsPDF from 'jspdf';
import 'jspdf-autotable';

interface HistorialComprasProps {
  numeroTarjeta: string;
}

const HistorialCompras: React.FC<HistorialComprasProps> = ({ numeroTarjeta }) => {
  const [compras, setCompras] = useState<Transacciones[]>([]);
  const [mesSeleccionado, setMesSeleccionado] = useState<number>(new Date().getMonth() + 1);
  const [busqueda, setBusqueda] = useState<string>('');
  const [comprasFiltradas, setComprasFiltradas] = useState<Transacciones[]>([]);
  const [modalVisible, setModalVisible] = useState<boolean>(false);
  const [nuevaCompra, setNuevaCompra] = useState<Compra>({
    numeroTarjeta,
    fechaCompra: new Date(),
    descripcion: '',
    monto: 0
  });

  useEffect(() => {
    async function fetchCompras() {
      try {
        const currentYear = new Date().getFullYear();
        const data = await getHistorialCompras(numeroTarjeta, mesSeleccionado, currentYear);
        if (data && data.item) {
          setCompras(data.item);
          setComprasFiltradas(data.item);  
        } else {
          console.error('Error al obtener las compras:', data);
        }
      } catch (error) {
        console.error('Error al obtener las compras:', error);
      }
    }

    fetchCompras();
  }, [mesSeleccionado, numeroTarjeta]);

  // Manejo de cambio de mes en el select
  const handleMesChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    setMesSeleccionado(parseInt(event.target.value));
    setBusqueda('');  
  };

  // Filtrar compras por descripción
  useEffect(() => {
    setComprasFiltradas(
      compras.filter(compra =>
        compra.descripcion.toLowerCase().includes(busqueda.toLowerCase())
      )
    );
  }, [busqueda, compras]);

  // Función para exportar a XLSX
  const exportToXLSX = () => {
    if (comprasFiltradas.length === 0) return;

    const ws = XLSX.utils.json_to_sheet(comprasFiltradas.map(compra => ({
      Fecha: new Date(compra.fechaTransaccion).toLocaleDateString('en-GB'),
      Descripción: compra.descripcion,
      Monto: `$${compra.monto.toFixed(2)}`,
    })));

    const wb = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Compras');
    XLSX.writeFile(wb, 'compras.xlsx');
  };

  // Función para exportar a PDF
  const exportToPDF = () => {
    if (comprasFiltradas.length === 0) return;

    const doc = new jsPDF();
    doc.text('Detalle de compras realizadas', 14, 16);
    
    const tableData = comprasFiltradas.map(compra => [
      new Date(compra.fechaTransaccion).toLocaleDateString('en-GB'),
      compra.descripcion,
      `$${compra.monto.toFixed(2)}`,
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

    doc.save('compras.pdf');
  };

  // Manejo del formulario del modal
  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setNuevaCompra(prevState => ({
      ...prevState,
      [name]: name === 'monto' ? parseFloat(value) : value,
      fechaCompra: name === 'fechaCompra' ? new Date(value) : prevState.fechaCompra
    }));
  };

  const handleSave = async () => {
    if (nuevaCompra.descripcion === '' || nuevaCompra.monto <= 0) {
      alert('Por favor, completa todos los campos.');
      return;
    }
  
    // Convertir la fecha a formato ISO para la API
    const fechaFormateada = nuevaCompra.fechaCompra.toISOString();
  
    const compraAEnviar = {
      numeroTarjeta: nuevaCompra.numeroTarjeta,
      fechaCompra: fechaFormateada,
      descripcion: nuevaCompra.descripcion,
      monto: nuevaCompra.monto
    };
  
    try {
      const response: GenericResponse = await crearCompra(compraAEnviar);
      
      if (response.code == 1) {
        alert('Compra agregada exitosamente.');
        setModalVisible(false);
        // Actualiza la lista de compras
        const currentYear = new Date().getFullYear();
        const data = await getHistorialCompras(numeroTarjeta, mesSeleccionado, currentYear);
        if (data && data.item) {
          setCompras(data.item);
          setComprasFiltradas(data.item);
        }
      } else {
        alert('Error al agregar la compra: ' + response.message);
      }
    } catch (error) {
      console.error('Error al agregar la compra:', error);
      alert('Error al agregar la compra: ' + error.message);
    }
  };

  const handleModalOpen = () => {
    setModalVisible(true);
  };

  const handleModalClose = () => {
    setModalVisible(false);
    // Limpia el formulario al cerrar
    setNuevaCompra({
      numeroTarjeta,
      fechaCompra: new Date(),
      descripcion: '',
      monto: 0
    });
  };

  return (
    <div style={{ padding: '10px', maxWidth: '100%' }}>
      <h3 style={{ textAlign: 'center', marginBottom: '20px', fontWeight: 'bold', fontSize: '1.5rem' }}>Detalle de compras realizadas</h3>

      <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: '20px' }}>
        <input
          type="text"
          placeholder="Buscar compra..."
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
            disabled={comprasFiltradas.length === 0}
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
            disabled={comprasFiltradas.length === 0}
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
            Agregar nueva compra
          </button>
        </div>
      </div>

      <table style={{ width: '100%', borderCollapse: 'collapse' }}>
        <thead>
          <tr>
            <th style={{ borderBottom: '1px solid #ddd', padding: '10px' }}>Fecha</th>
            <th style={{ borderBottom: '1px solid #ddd', padding: '10px' }}>Descripción</th>
            <th style={{ borderBottom: '1px solid #ddd', padding: '10px' }}>Monto</th>
          </tr>
        </thead>
        <tbody>
          {comprasFiltradas.length > 0 ? (
            comprasFiltradas.map((compra, index) => (
              <tr key={index}>
                <td style={{ padding: '10px' }}>{new Date(compra.fechaTransaccion).toLocaleDateString('en-GB')}</td>
                <td style={{ padding: '10px' }}>{compra.descripcion}</td>
                <td style={{ padding: '10px' }}>
                  ${compra.monto.toFixed(2)}
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan={3} style={{ textAlign: 'center', padding: '10px' }}>No se encontraron compras</td>
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
            maxWidth: '500px',
            width: '100%',
          }}>
            <h4 style={{ marginTop: '0' }}>Agregar nueva compra</h4>
            <form
              onSubmit={(e) => {
                e.preventDefault();
                handleSave();
              }}
            >
              <div style={{ marginBottom: '10px' }}>
                <label>Fecha:</label>
                <input
                  type="date"
                  name="fechaCompra"
                  value={nuevaCompra.fechaCompra.toISOString().split('T')[0]}
                  onChange={handleInputChange}
                  style={{
                    borderRadius: '5px',
                    padding: '10px',
                    border: '1px solid #ccc',
                    width: '100%',
                  }}
                />
              </div>
              <div style={{ marginBottom: '10px' }}>
                <label>Descripción:</label>
                <input
                  type="text"
                  name="descripcion"
                  value={nuevaCompra.descripcion}
                  onChange={handleInputChange}
                  style={{
                    borderRadius: '5px',
                    padding: '10px',
                    border: '1px solid #ccc',
                    width: '100%',
                  }}
                />
              </div>
              <div style={{ marginBottom: '10px' }}>
                <label>Monto:</label>
                <input
                  type="number"
                  name="monto"
                  value={nuevaCompra.monto}
                  onChange={handleInputChange}
                  style={{
                    borderRadius: '5px',
                    padding: '10px',
                    border: '1px solid #ccc',
                    width: '100%',
                  }}
                />
              </div>
              <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                <button
                  type="button"
                  onClick={handleModalClose}
                  style={{
                    backgroundColor: '#6c757d',
                    color: '#fff',
                    border: 'none',
                    borderRadius: '5px',
                    padding: '10px 20px',
                    cursor: 'pointer',
                  }}
                >
                  Cancelar
                </button>
                <button
                  type="submit"
                  style={{
                    backgroundColor: '#007bff',
                    color: '#fff',
                    border: 'none',
                    borderRadius: '5px',
                    padding: '10px 20px',
                    cursor: 'pointer',
                  }}
                >
                  Guardar
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
};

export default HistorialCompras;
