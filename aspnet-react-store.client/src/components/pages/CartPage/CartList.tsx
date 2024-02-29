import { Container, Divider, Stack, Typography } from '@mui/material';
import { ICart } from '../../../types/types';
import CartProduct from './CartProduct';

interface CartListProps {
  cart: ICart | null;
}

export default function CartList({ cart }: CartListProps) {
  return (
    <>
      <Typography variant='h4'>Корзина</Typography>
      <Divider />
      {!cart?.products.length && (
        <Typography variant='h5' mt={5}>
          Корзина пуста
        </Typography>
      )}
      <Stack mt={5} spacing={3}>
        {cart?.products.length !== 0 &&
          cart?.products?.map((product, index) => (
            <Container key={index}>
              <CartProduct product={product} />
            </Container>
          ))}
      </Stack>
    </>
  );
}
