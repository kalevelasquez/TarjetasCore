import React from 'react';

interface CreditInfoProps {
  limite: number;
  saldoActual: number;
  saldoDisponible: number;
  fechaCorte: string;
  interesConfigurableMinimo: number;
  interesConfigurable: number;
  interesBonificable: number;
  cuotaMinimaPagar: number;
  montoTotalContado: number;
}

const CreditInfo: React.FC<CreditInfoProps> = ({
  limite,
  saldoActual,
  saldoDisponible,
  fechaCorte,
  interesConfigurableMinimo,
  interesConfigurable,
  interesBonificable,
  cuotaMinimaPagar,
  montoTotalContado
}) => {
  // Función para formatear los valores en dólares
  const formatCurrency = (value: number) => `$${value.toFixed(2)}`;
  
  // Función para formatear los porcentajes
  const formatPercentage = (value: number) => `${(value * 100).toFixed(0)}%`;

  // Función para formatear la fecha en formato DD-MM-YYYY
  const formatDate = (dateString: string) => {
    const date = new Date(dateString);
    const day = date.getDate().toString().padStart(2, '0');
    const month = (date.getMonth() + 1).toString().padStart(2, '0'); 
    const year = date.getFullYear();
    return `${day}-${month}-${year}`;
  };

  return (
    <div style={{ textAlign: 'center' }}>
      <h3 style={{ fontWeight: 'bold', fontSize: '1.5rem', marginBottom: '20px' }}>Detalle de crédito</h3>
      <div style={{ display: 'flex', justifyContent: 'space-between' }}>
        <div style={{ width: '50%', textAlign: 'left' }}>
          <p style={{ marginBottom: '10px' }}><strong>Límite:</strong> {formatCurrency(limite)}</p>
          <p style={{ marginBottom: '10px' }}><strong>Saldo Actual:</strong> {formatCurrency(saldoActual)}</p>
          <p style={{ marginBottom: '10px' }}><strong>Saldo Disponible:</strong> {formatCurrency(saldoDisponible)}</p>
          <p style={{ marginBottom: '10px' }}><strong>Fecha de Corte:</strong> {formatDate(fechaCorte)}</p>
        </div>
        <div style={{ width: '50%', textAlign: 'left' }}>
          <p style={{ marginBottom: '10px' }}><strong>Interés Configurable Mínimo:</strong> {formatPercentage(interesConfigurableMinimo)}</p>
          <p style={{ marginBottom: '10px' }}><strong>Interés Configurable:</strong> {formatPercentage(interesConfigurable)}</p>
          <p style={{ marginBottom: '10px' }}><strong>Interés Bonificable:</strong> {formatCurrency(interesBonificable)}</p>
          <p style={{ marginBottom: '10px' }}><strong>Cuota Mínima a Pagar:</strong> {formatCurrency(cuotaMinimaPagar)}</p>
          <p style={{ marginBottom: '10px' }}><strong>Monto Total de Contado:</strong> {formatCurrency(montoTotalContado)}</p>
        </div>
      </div>
    </div>
  );
};

export default CreditInfo;
