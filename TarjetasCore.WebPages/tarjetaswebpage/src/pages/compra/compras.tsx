import React from 'react';
import Menu from '../components/Menu'; 
import HistorialCompras from '../components/HistorialCompras'; 
const ComprasPage: React.FC = () => {
  const numeroTarjeta = '4545252332120011';

  return (
    <div>
      <Menu />
      <HistorialCompras numeroTarjeta={numeroTarjeta} />
    </div>
  );
};

export default ComprasPage;
