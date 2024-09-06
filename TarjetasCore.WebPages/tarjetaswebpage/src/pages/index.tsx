import React, { useEffect, useState } from 'react';
import Menu from '../pages/components/Menu';
import CreditCard from '../pages/components/CreditCard';
import CreditInfo from '../pages/components/CreditInfo';
import TransaccionesInfo from '../pages/components/TransaccionesInfo';
import { getInformacionTargeta } from './data/data';

const HomePage = () => {
  const [cardInfo, setCardInfo] = useState(null);

  useEffect(() => {
    async function fetchCardData() {
      const data = await getInformacionTargeta('4545252332120011');
      setCardInfo(data.item);
    }

    fetchCardData();
  }, []);

  return (
    <div>
      <Menu />
      <div style={{ display: 'flex', flexDirection: 'row', marginTop: '20px' }}>
        <div style={{ width: '50%' }}>
          {cardInfo ? (
            <CreditCard
              nombreAsociado={cardInfo.nombreAsociado}
              numeroTarjeta={cardInfo.numeroTarjeta}
              fechaVencimiento={cardInfo.fechaVencimiento}
              cvv={cardInfo.cvv}
            />
          ) : (
            <p>Cargando información de la tarjeta...</p>
          )}
        </div>
        <div style={{ width: '50%' }}>
          {cardInfo ? (
            <CreditInfo
              limite={cardInfo.limite}
              saldoActual={cardInfo.saldoActual}
              saldoDisponible={cardInfo.saldoDisponible}
              fechaCorte={cardInfo.fechaCorte}
              interesConfigurableMinimo={cardInfo.interesConfigurableMinimo}
              interesConfigurable={cardInfo.interesConfigurable}
              interesBonificable={cardInfo.interesBonificable}
              cuotaMinimaPagar={cardInfo.cuotaMinimaPagar}
              montoTotalContado={cardInfo.montoTotalContado}
            />
          ) : (
            <p>Cargando detalles de crédito...</p>
          )}
        </div>
      </div>

      <hr />
      <div style={{ marginTop: '40px' }}>
        {cardInfo ? (
          <TransaccionesInfo 
            numeroTarjeta={cardInfo.numeroTarjeta} 
            saldoActual={cardInfo.saldoActual} 
          />
        ) : (
          <p>Cargando historial de transacciones...</p>
        )}
      </div>
    </div>
  );
};

export default HomePage;
