import { Favorite, ShoppingCart } from '@mui/icons-material';
import {
  Button,
  Chip,
  Container,
  Divider,
  Grid,
  IconButton,
  Stack,
  Tooltip,
  Typography,
} from '@mui/material';
import { useState } from 'react';
import { IProduct, IUser } from '../../../types/types';
import ProductImages from '../MainPage/ProductImages';

interface ProductContentProps {
  product: IProduct;
  currentUser: IUser | null;
}

export default function ProductContent({
  product,
  currentUser,
}: ProductContentProps) {
  const [favorite, setFavorite] = useState(false);

  return (
    <>
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
              {currentUser && (
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
              )}
            </Typography>
            <Divider />
            <Stack direction='row' mt={2} justifyContent='space-between'>
              <Typography variant='subtitle1' color={'GrayText'}>
                {product.quantity !== 0 && `Количество: ${product.quantity}`}
                {product.quantity === 0 && 'Нет в наличии'}
              </Typography>
              <Stack direction='row' spacing={1}>
                {product.categories.map((category, index) => (
                  <Chip label={category.name} key={index} color='primary' />
                ))}
              </Stack>
            </Stack>
            <Typography variant='body1' mt={3}>
              {product.description}
            </Typography>
            <Container sx={{ textAlign: 'right' }}>
              {product.discount === 0 && (
                <Typography variant='h5' mt={5}>
                  Цена: <em>₽{product.price.toFixed(2)} руб.</em>
                </Typography>
              )}
              {product.discount !== 0 && (
                <>
                  <Typography variant='h5' mt={5}>
                    Цена:{' '}
                    <em>
                      ₽{(product.price * (1 - product.discount)).toFixed(2)}
                    </em>{' '}
                    <s style={{ color: 'gray' }}>₽{product.price.toFixed(2)}</s>
                  </Typography>
                  <Typography variant='body2' color='gray'>
                    Скидка: {product.discount * 100}%
                  </Typography>
                </>
              )}
              {currentUser && (
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
