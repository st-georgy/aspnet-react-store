import { AddShoppingCartOutlined, FavoriteBorder } from '@mui/icons-material';
import {
  Card,
  CardContent,
  IconButton,
  Stack,
  Typography,
} from '@mui/material';
import { Link } from 'react-router-dom';
import { animated, useTrail } from 'react-spring';
import { IProduct } from '../../../types/types';
import '../../style/product.css';
import ProductImages from './ProductImages';

export default function Product({
  product,
  index,
}: {
  product: IProduct;
  index: number;
}) {
  const trail = useTrail(index + 1, {
    opacity: 1,
    y: 0,
    from: { opacity: 0, y: 20 },
  });

  return (
    <animated.div style={trail[index]} className='product-block'>
      <Card elevation={0}>
        <Link to={`/products/${product.id}`}>
          <div className='image-container'>
            <ProductImages images={product.images} />
          </div>
        </Link>
        <CardContent className='product-content'>
          <Stack direction='row'>
            <Link to={`/products/${product.id}`}>
              <Typography variant='h6' className='product-name'>
                {product.name}
              </Typography>
            </Link>
            <Stack direction='row' sx={{ marginLeft: 'auto' }}>
              <IconButton>
                <FavoriteBorder color='inherit' fontSize='small' />
              </IconButton>
              <IconButton>
                <AddShoppingCartOutlined color='inherit' fontSize='small' />
              </IconButton>
            </Stack>
          </Stack>
          <Stack direction='row'>
            <Typography variant='body2' mr='auto' color='GrayText'>
              {product.quantity !== 0 ? 'В наличии' : 'Нет в наличии'}
            </Typography>
            {product.discount === 0 && (
              <Typography variant='body2' mr={1}>
                <em>{'₽' + product.price.toFixed(2)}</em>
              </Typography>
            )}
            {product.discount !== 0 && (
              <>
                <Typography variant='body2' mr={1}>
                  <em>₽{(product.price * product.discount).toFixed(2)}</em>
                  &nbsp;
                  <s style={{ color: 'gray' }}>₽{product.price.toFixed(2)}</s>
                </Typography>
              </>
            )}
          </Stack>
        </CardContent>
      </Card>
    </animated.div>
  );
}
