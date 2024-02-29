import { Checkbox, Stack } from '@mui/material';
import { ICartProduct } from '../../../types/types';
import CartProductImage from './CartProductImage';
import CartProductInfo from './CartProductInfo';
import QuantityControls from './QuantityControls';

interface CartProductProps {
  product: ICartProduct;
}

export default function CartProduct({ product }: CartProductProps) {
  return (
    <>
      <Stack direction='row' spacing={3} alignItems='center'>
        <Checkbox />
        <CartProductImage
          productId={product.id}
          filePath={product.image?.filePath}
        />
        <CartProductInfo
          id={product.id}
          name={product.name}
          price={product.price}
          discount={product.discount}
        />
        <QuantityControls quantity={product.quantityInCart} />
      </Stack>
    </>
  );
}
