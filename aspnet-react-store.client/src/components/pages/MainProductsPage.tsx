import { useLocation } from 'react-router-dom';
import ProductsList from '../MainProductsPage/ProductsList';
import NavigationBar from '../NavigationBar';

export default function ProductsPage() {
  const location = useLocation();
  const searchParams = new URLSearchParams(location.search);
  const searchText = searchParams.get('searchText');

  return (
    <>
      <NavigationBar />
      {searchText && searchText.trim() !== '' ? (
        <ProductsList searchText={searchText} />
      ) : (
        <ProductsList />
      )}
    </>
  );
}
