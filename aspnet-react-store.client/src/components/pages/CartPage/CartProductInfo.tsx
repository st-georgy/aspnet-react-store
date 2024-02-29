import { Link, Stack, Typography } from '@mui/material';

interface CartProductInfoProps {
  id: number;
  name: string;
  price: number;
  discount: number;
}

export default function CartProductInfo({
  id,
  name,
  price,
  discount,
}: CartProductInfoProps) {
  return (
    <Stack direction='column' flexGrow={1}>
      <Typography variant='h6'>
        <Link href={`/products/${id}`} color='inherit'>
          {name}
        </Link>
      </Typography>
      <Typography variant='body1' color='text.secondary'>
        Цена:&nbsp;
        <em>₽{(price * (1 - discount)).toFixed(2)} </em>
        {discount !== 0 && <s>₽{price.toFixed(2)}</s>}
      </Typography>
    </Stack>
  );
}
