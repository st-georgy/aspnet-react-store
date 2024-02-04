import { Card } from '@mui/material';
import { Navigation, Pagination } from 'swiper/modules';
import { Swiper, SwiperSlide } from 'swiper/react';
import 'swiper/swiper-bundle.css';
import { IImage } from '../../types/types';

interface ProductImagesProps {
  images: IImage[];
}

export default function ProductImages({ images }: ProductImagesProps) {
  return (
    <Swiper
      navigation
      pagination={{ clickable: true }}
      loop={true}
      spaceBetween={30}
      slidesPerView={1}
      modules={[Navigation, Pagination]}
    >
      {(!images || images.length === 0) && (
        <>
          <SwiperSlide>
            <Card elevation={3}>
              <img
                src='src/assets/products/placeholder_light.png'
                alt='Placeholder'
                style={{ width: '100%' }}
              />
            </Card>
          </SwiperSlide>
          <SwiperSlide>
            <Card elevation={3}>
              <img
                src='src/assets/products/placeholder_light.png'
                alt='Placeholder'
                style={{ width: '100%' }}
              />
            </Card>
          </SwiperSlide>
        </>
      )}
      {images &&
        images.length > 0 &&
        images.map((image) => (
          <SwiperSlide key={image.id}>
            <Card elevation={3}>
              <img
                src={image.filePath}
                alt={`Product Image ${image.id}`}
                style={{ width: '100%' }}
              />
            </Card>
          </SwiperSlide>
        ))}
    </Swiper>
  );
}
