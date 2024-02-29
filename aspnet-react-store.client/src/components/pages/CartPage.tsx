import { Container, Grid } from '@mui/material';
import { useEffect, useState } from 'react';
import { ICart, IUser } from '../../types/types';
import { getCart } from '../../utils/cartApiUtils';
import NavBar from '../navigation/NavBar';
import CartList from './CartPage/CartList';
import CartSummary from './CartPage/CartSummary';

interface CartPageProps {
  currentUser: IUser;
  isAdmin: boolean;
}
export default function CartPage({ currentUser, isAdmin }: CartPageProps) {
  const [cart, setCart] = useState<ICart | null>(null);

  useEffect(() => {
    const fetchCart = async () => {
      try {
        const response = await getCart();
        setCart(response);
      } catch (error) {
        console.error('Failed to catch cart: ', error);
      }
    };

    fetchCart();
  }, []);

  return (
    <>
      <NavBar currentUser={currentUser} isAdmin={isAdmin} showSearch={false} />
      <Container sx={{ mt: '6rem' }}>
        <Grid container spacing={4}>
          <Grid item xs={7} md={8}>
            <CartList cart={cart} />
          </Grid>
          <Grid item xs={5} md={4}>
            <CartSummary cart={cart} />
          </Grid>
        </Grid>
      </Container>
    </>
  );
}
