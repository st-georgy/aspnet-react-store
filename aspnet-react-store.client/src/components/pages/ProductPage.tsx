import { Favorite, ShoppingCart } from '@mui/icons-material';
import {
  Button,
  Container,
  Divider,
  Grid,
  IconButton,
  Tooltip,
  Typography,
} from '@mui/material';
import axios from 'axios';
import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { IProduct, IUser } from '../../types/types';
import NavBar from '../navigation/NavBar';
import ProductImages from './MainPage/ProductImages';
import NotFoundPage from './NotFoundPage';

interface ProductPageProps {
  currentUser: IUser | null;
  isAdmin: boolean;
}

export default function ProductPage({
  currentUser,
  isAdmin,
}: ProductPageProps) {
  const { id } = useParams<{ id: string }>();

  if (isNaN(Number(id))) {
    return <NotFoundPage />;
  }

  const [product, setProduct] = useState<IProduct | null>(null);
  const [error, setError] = useState(false);
  const [favorite, setFavorite] = useState(false);

  useEffect(() => {
    const fetchProduct = async () => {
      try {
        const response = await axios.get(`/api/products/${id}`);
        setProduct(response.data);
      } catch (error) {
        console.error('Failed to fetch product:', error);
        setError(true);
      }
    };

    fetchProduct();
  }, [id]);

  if (error) {
    return <NotFoundPage />;
  }

  if (!product) {
    return <></>;
  }

  return (
    <>
      <NavBar currentUser={currentUser} isAdmin={isAdmin} showSearch={false} />
      <Container sx={{ mt: '6rem' }}>
        <Grid container spacing={3}>
          <Grid item xs={12} sm={6}>
            <div className='image-container big-img'>
              <ProductImages images={product.images} />
            </div>
          </Grid>
          <Grid item xs={12} sm={6}>
            <Typography
              variant='h2'
              sx={{ display: 'flex', alignItems: 'center' }}
            >
              {product.name}
              <Tooltip
                title={
                  favorite ? 'Удалить из избранного' : 'Добавить в избранное'
                }
                placement='left'
                arrow
              >
                <IconButton
                  sx={{ ml: 'auto' }}
                  onClick={() => setFavorite(!favorite)}
                >
                  <Favorite
                    fontSize='large'
                    htmlColor={favorite ? 'red' : 'grey'}
                  />
                </IconButton>
              </Tooltip>
            </Typography>
            <Divider />
            <Typography variant='body1' mt={5}>
              {product.description}
              Aliquip nulla magna duis anim officia laborum adipisicing aliqua.
              Reprehenderit aute dolore adipisicing laboris magna enim voluptate
              labore dolore eu laborum culpa ut sunt. Aliqua cupidatat eiusmod
              deserunt cupidatat cillum consectetur id. Et mollit proident culpa
              quis qui eiusmod sunt veniam do. Ullamco aliqua magna laborum
              Lorem. Dolore tempor ex Lorem nulla. Laborum consequat et ut minim
              Lorem pariatur nostrud deserunt voluptate proident aliquip.
            </Typography>
            <Container sx={{ textAlign: 'right' }}>
              <Typography variant='h5' mt={5}>
                Цена: <em>₽{product.price} руб.</em>
              </Typography>
              {!!currentUser && (
                <Button
                  variant='contained'
                  color='primary'
                  sx={{ mt: 2 }}
                  startIcon={<ShoppingCart />}
                >
                  Добавить в корзину
                </Button>
              )}
            </Container>
          </Grid>
        </Grid>
      </Container>
    </>
  );
}
