import { useLocation } from 'react-router-dom';
import ProductsList from '../ProductsPage/ProductsList';

export default function ProductsPage() {
  const location = useLocation();
  const searchParams = new URLSearchParams(location.search);
  const searchText = searchParams.get('searchText');

  return (
    <>
      {searchText && searchText.trim() !== '' ? (
        <ProductsList searchText={searchText} />
      ) : (
        <ProductsList />
      )}
    </>
  );
}
