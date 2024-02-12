import { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import { getRole, validateToken } from '../../utils/authApiUtils';
import NavBar from '../navigation/NavBar';
import ShowAlert from '../shared/ShowAlert';
import ProductsList from './MainProductsPage/ProductsList';

export default function ProductsPage() {
  const location = useLocation();
  const queryParams = new URLSearchParams(location.search);
  const searchText = queryParams.get('searchText');
  const loginSuccess = queryParams.get('loginSuccess');

  const [showLoginAlert, setShowLoginAlert] = useState<boolean>(false);
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);
  const [isAdmin, setIsAdmin] = useState<boolean>(false);

  useEffect(() => {
    const fetchData = async () => {
      const isTokenValid = await validateToken();
      setIsLoggedIn(isTokenValid);

      if (isTokenValid) {
        const role = await getRole();
        setIsAdmin(role === 'ADMIN');
      }
    };

    fetchData();

    if (loginSuccess === 'true') setShowLoginAlert(true);
  }, []);

  return (
    <>
      <NavBar isLoggedIn={isLoggedIn} isAdmin={isAdmin} />
      <ProductsList searchText={searchText ?? undefined} />
      {showLoginAlert && (
        <ShowAlert severity='success' message='Вы успешно авторизовались' />
      )}
    </>
  );
}
