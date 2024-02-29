import { Button, Divider, Typography } from '@mui/material';
import { ICart } from '../../../types/types';

interface CartSummaryProps {
  cart: ICart | null;
}

export default function CartSummary({ cart }: CartSummaryProps) {
  if (!cart || !cart.products || !cart.products.length) return null;
  return (
    <>
      <Typography variant='h4'>Итого</Typography>
      <Divider />
      <Typography
        variant='body1'
        mt={3}
        style={{ display: 'flex', justifyContent: 'space-between' }}
      >
        <span className='summary-field'>
          Общая стоимость ({cart?.totalProducts} тов.):
        </span>
        <span>₽{cart?.totalPrice.toFixed(2)}</span>
      </Typography>
      {cart?.discount !== 0 && (
        <>
          <Typography
            variant='body1'
            mt={3}
            display='flex'
            justifyContent='space-between'
          >
            <span className='summary-field'>
              Скидка ({(cart?.discount ?? 0) * 100}%):
            </span>
            ₽{((cart?.totalPrice ?? 0) * (cart?.discount ?? 0)).toFixed(2)}
          </Typography>
          <Typography
            variant='body1'
            mt={3}
            display='flex'
            justifyContent='space-between'
          >
            <span className='summary-field'>Цена с учетом скидки:</span>₽
            {((cart?.totalPrice ?? 0) * (1 - (cart?.discount ?? 0))).toFixed(2)}
          </Typography>
        </>
      )}
      <Button variant='contained' fullWidth sx={{ mt: 5 }}>
        Перейти к оформлению&nbsp;
      </Button>
    </>
  );
}
