import React from 'react';
import Link from 'next/link';

const Menu: React.FC = () => {
  return (
    <nav style={{ display: 'flex', justifyContent: 'space-around', padding: '10px', backgroundColor: '#29346e', width: '100%' }}>
      <Link href="/" style={{ color: '#fff', textDecoration: 'none' }}>Inicio</Link>
      <Link href="/pago/pagos" style={{ color: '#fff', textDecoration: 'none' }}>Pagos</Link>
      <Link href="/compra/compras" style={{ color: '#fff', textDecoration: 'none' }}>Compras</Link>
    </nav>
  );
};

export default Menu;