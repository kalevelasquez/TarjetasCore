import React, { useState, useEffect } from 'react';
import { getHistorialTransacciones } from '../data/data';
import { Transacciones, HistorialTransaccionesResponse } from '../data/definitions';
import * as XLSX from 'xlsx';
import jsPDF from 'jspdf';
import 'jspdf-autotable';

interface TransaccionesInfoProps {
  numeroTarjeta: string;
  saldoActual: number;
}

const TransaccionesInfo: React.FC<TransaccionesInfoProps> = ({ numeroTarjeta, saldoActual }) => {
  const [transacciones, setTransacciones] = useState<His[]>([]);
  const [mesSeleccionado, setMesSeleccionado] = useState<number>(new Date().getMonth() + 1);
  const [busqueda, setBusqueda] = useState<string>('');
  const [transaccionesFiltradas, setTransaccionesFiltradas] = useState<Transacciones[]>([]);
  
  useEffect(() => {
    async function fetchTransacciones() {
      try {
        const currentYear = new Date().getFullYear();
        const data = await getHistorialTransacciones(numeroTarjeta, mesSeleccionado, currentYear);
        if (data && data.item) {
          setTransacciones(data.item);
          setTransaccionesFiltradas(data.item);  
        } else {
          console.error('Error al obtener las transacciones:', data);
        }
      } catch (error) {
        console.error('Error al obtener las transacciones:', error);
      }
    }

    fetchTransacciones();
  }, [mesSeleccionado, numeroTarjeta]);

  // Manejo de cambio de mes en el select
  const handleMesChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
    setMesSeleccionado(parseInt(event.target.value));
    setBusqueda('');  
  };

  // Filtrar transacciones por descripción
  useEffect(() => {
    setTransaccionesFiltradas(
      transacciones.filter(transaccion =>
        transaccion.descripcion.toLowerCase().includes(busqueda.toLowerCase())
      )
    );
  }, [busqueda, transacciones]);

  // Función para exportar a XLSX
  const exportToXLSX = () => {
    if (transaccionesFiltradas.length === 0) return;

    const ws = XLSX.utils.json_to_sheet(transaccionesFiltradas.map(transaccion => ({
      Fecha: new Date(transaccion.fechaTransaccion).toLocaleDateString('en-GB'),
      Descripción: transaccion.descripcion,
      Monto: `$${transaccion.monto.toFixed(2)}`,
    })));

    const wb = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Transacciones');
    XLSX.writeFile(wb, 'transacciones.xlsx');
  };

  // Función para exportar a PDF
  const exportToPDF = () => {
    if (transaccionesFiltradas.length === 0) return;

    const doc = new jsPDF();
    doc.text('Detalle de transacciones del mes', 14, 16);
    
    const tableData = transaccionesFiltradas.map(transaccion => [
      new Date(transaccion.fechaTransaccion).toLocaleDateString('en-GB'),
      transaccion.descripcion,
      `$${transaccion.monto.toFixed(2)}`,
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

    doc.text(`Saldo Actual: $${saldoActual.toFixed(2)}`, 14, doc.internal.pageSize.height - 10);
    
    doc.save('transacciones.pdf');
  };

  return (
    <div style={{ padding: '10px', maxWidth: '100%' }}>
      <h3 style={{ textAlign: 'center', marginBottom: '20px', fontWeight: 'bold', fontSize: '1.5rem' }}>Detalle de transacciones del mes</h3>

      <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: '20px' }}>
        <input
          type="text"
          placeholder="Buscar transacción..."
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
            disabled={transaccionesFiltradas.length === 0}
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
            disabled={transaccionesFiltradas.length === 0}
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
          {transaccionesFiltradas.length > 0 ? (
            transaccionesFiltradas.map((transaccion) => (
              <tr key={transaccion.idTransaccion}>
                <td style={{ padding: '10px' }}>{new Date(transaccion.fechaTransaccion).toLocaleDateString('en-GB')}</td>
                <td style={{ padding: '10px' }}>{transaccion.descripcion}</td>
                <td
                  style={{
                    padding: '10px',
                    color: transaccion.tipoTransact === 'C' ? 'red' : 'green',
                  }}
                >
                  ${transaccion.monto.toFixed(2)}
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan={3} style={{ textAlign: 'center', padding: '10px' }}>No se encontraron transacciones</td>
            </tr>
          )}
        </tbody>
      </table>

      <p style={{ textAlign: 'right', marginTop: '20px', fontWeight: 'bold' }}>Saldo Actual: ${saldoActual.toFixed(2)}</p>
    </div>
  );
};

export default TransaccionesInfo;
