import React from 'react';
import Menu from '../components/Menu'; 
import HistorialPagos from '../components/HistorialPagos'; 

const PagosPage: React.FC = () => {
  const numeroTarjeta = '4545252332120011';

  return (
    <div>
      <Menu />
      <HistorialPagos numeroTarjeta={numeroTarjeta} />
    </div>
  );
};

export default PagosPage;
