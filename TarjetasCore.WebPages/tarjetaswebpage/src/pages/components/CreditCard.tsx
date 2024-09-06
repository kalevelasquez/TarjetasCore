import React, { useState } from 'react';

interface CreditCardProps {
  nombreAsociado: string;
  numeroTarjeta: string;
  fechaVencimiento: string;
  cvv: number;
}

const CreditCard: React.FC<CreditCardProps> = ({ nombreAsociado, numeroTarjeta, fechaVencimiento, cvv }) => {
  const [showCVV, setShowCVV] = useState(false);

  // Función para formatear el número de la tarjeta en grupos de 4
  const formatCardNumber = (num: string) => {
    return num.replace(/\d{4}(?=.)/g, '$& ');
  };

  return (
    <div style={{
      backgroundColor: '#29346e',
      color: '#fff',
      padding: '30px',
      borderRadius: '15px',
      width: '400px',
      height: '220px',
      display: 'flex',
      flexDirection: 'column',
      justifyContent: 'space-between',
      boxShadow: '0px 4px 6px rgba(0, 0, 0, 0.1)',
      marginLeft: '20px',
    }}>
      <div>
        <h3 style={{ margin: 0, fontSize: '1.5rem' }}>{nombreAsociado}</h3>
        <p style={{ fontSize: '1.2rem', letterSpacing: '2px' }}>{formatCardNumber(numeroTarjeta)}</p> 
      </div>
      <div style={{ display: 'flex', justifyContent: 'space-between' }}>
        <p>Vence: {fechaVencimiento}</p>
        <p onClick={() => setShowCVV(!showCVV)} style={{ cursor: 'pointer', textAlign: 'right' }}>
          CVV: {showCVV ? cvv : '***'}
        </p>
      </div>
    </div>
  );
};

export default CreditCard;
