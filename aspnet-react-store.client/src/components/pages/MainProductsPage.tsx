import { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import { IUser } from '../../types/types';
import NavBar from '../navigation/NavBar';
import ShowAlert from '../shared/ShowAlert';
import ProductsList from './MainProductsPage/ProductsList';

interface ProductsPageProps {
  currentUser: IUser | null;
  isAdmin: boolean;
}

export default function ProductsPage({
  currentUser,
  isAdmin,
}: ProductsPageProps) {
  const location = useLocation();
  const queryParams = new URLSearchParams(location.search);
  const searchText = queryParams.get('searchText');
  const loginSuccess = queryParams.get('loginSuccess');

  const [showLoginAlert, setShowLoginAlert] = useState<boolean>(false);

  useEffect(() => {
    if (loginSuccess === 'true') setShowLoginAlert(true);
  }, []);

  return (
    <>
      <NavBar currentUser={currentUser} isAdmin={isAdmin} showSearch={true} />
      <ProductsList searchText={searchText ?? undefined} />
      {showLoginAlert && (
        <ShowAlert severity='success' message='Вы успешно авторизовались' />
      )}
    </>
  );
}
