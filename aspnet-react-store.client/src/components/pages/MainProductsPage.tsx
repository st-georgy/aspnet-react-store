import { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import { validateToken } from '../../utils/authApiUtils';
import ShowAlert from '../LoginPage/ShowAlert';
import ProductsList from '../MainProductsPage/ProductsList';
import NavigationBar from '../NavigationBar';

export default function ProductsPage() {
  const location = useLocation();
  const queryParams = new URLSearchParams(location.search);
  const searchText = queryParams.get('searchText');
  const loginSuccess = queryParams.get('loginSuccess');

  const [showLoginAlert, setShowLoginAlert] = useState<boolean>(false);
  const [loggedIn, setLoggedIn] = useState<boolean>(false);

  useEffect(() => {
    if (loginSuccess === 'true') setShowLoginAlert(true);

    validateToken().then((isTokenValid) => setLoggedIn(isTokenValid));
  });

  return (
    <>
      <NavigationBar loggedIn={loggedIn} />
      {searchText && searchText.trim() !== '' ? (
        <ProductsList searchText={searchText} />
      ) : (
        <ProductsList />
      )}
      {showLoginAlert && (
        <ShowAlert severity='success' message='Вы успешно авторизовались' />
      )}
    </>
  );
}
