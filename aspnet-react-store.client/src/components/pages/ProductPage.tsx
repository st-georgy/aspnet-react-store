import axios from 'axios';
import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { IProduct, IUser } from '../../types/types';
import NavBar from '../navigation/NavBar';
import NotFoundPage from './NotFoundPage';
import ProductContent from './ProductPage/ProductContent';

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

  return error || !product ? (
    error ? (
      <NotFoundPage />
    ) : (
      <></>
    )
  ) : (
    <>
      <NavBar currentUser={currentUser} isAdmin={isAdmin} showSearch={false} />
      <ProductContent product={product} currentUser={currentUser} />
    </>
  );
}
