import { Card, Carousel } from 'react-bootstrap';
import { IImage } from '../../types/types';

interface ProductImagesProps {
  images: IImage[];
}

export default function ProductImages({ images }: ProductImagesProps) {
  return (
    <Carousel interval={null} data-bs-theme='dark'>
      {(!images || images.length === 0) && (
        <Carousel.Item>
          <Card.Img
            variant='top'
            src='src/assets/products/placeholder_light.png'
          />
        </Carousel.Item>
      )}
      {images &&
        images.length > 0 &&
        images.map((image) => (
          <Carousel.Item key={image.id}>
            <Card.Img variant='top' src={image.filePath} />
          </Carousel.Item>
        ))}
    </Carousel>
  );
}
