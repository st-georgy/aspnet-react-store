import { Link } from '@mui/material';

interface CartProductImageProps {
  productId: number;
  filePath?: string | null;
}

export default function CartProductImage({
  productId,
  filePath,
}: CartProductImageProps) {
  return (
    <Link href={`/products/${productId}`}>
      <img
        src={filePath || '/src/assets/products/placeholder_light.png'}
        alt='Product image'
        style={{ width: '100px' }}
      />
    </Link>
  );
}
