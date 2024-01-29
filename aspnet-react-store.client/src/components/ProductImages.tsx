import { Card, Carousel } from 'react-bootstrap';

export default function ProductImages() {
  return (
    <Carousel interval={null} data-bs-theme='dark'>
      <Carousel.Item>
        <Card.Img variant='top' src='src/assets/products/tshirt.png' />
      </Carousel.Item>
      <Carousel.Item>
        <Card.Img variant='top' src='src/assets/products/tshirt.png' />
      </Carousel.Item>
      <Carousel.Item>
        <Card.Img variant='top' src='src/assets/products/tshirt.png' />
      </Carousel.Item>
    </Carousel>
  );
}
