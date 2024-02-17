import { Card } from '@mui/material';
import { Navigation, Pagination } from 'swiper/modules';
import { Swiper, SwiperSlide } from 'swiper/react';
import 'swiper/swiper-bundle.css';
import { IImage } from '../../../types/types';
import '../../style/swiper.css';

interface ProductImagesProps {
  images: IImage[];
}

const PlaceholderImage = () => (
  <SwiperSlide>
    <Card elevation={0}>
      <img
        src='/src/assets/products/placeholder_light.png'
        alt='Placeholder'
        style={{ width: '100%' }}
      />
    </Card>
  </SwiperSlide>
);

export default function ProductImages({ images }: ProductImagesProps) {
  const placeholderCount = 1;

  return (
    <Swiper
      navigation
      pagination={{ clickable: true }}
      loop={images && images.length !== 0}
      spaceBetween={30}
      slidesPerView={1}
      modules={[Navigation, Pagination]}
    >
      {!images || images.length === 0 ? (
        <>
          {[...Array(placeholderCount)].map((_, index) => (
            <PlaceholderImage key={index} />
          ))}
        </>
      ) : (
        images.map((image) => (
          <SwiperSlide key={image.id}>
            <Card elevation={0}>
              <img
                src={image.filePath}
                alt={`Product Image ${image.id}`}
                style={{ width: '100%' }}
              />
            </Card>
          </SwiperSlide>
        ))
      )}
    </Swiper>
  );
}
