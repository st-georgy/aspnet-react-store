import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';
import { useEffect, useState } from 'react';
import { Route, Routes } from 'react-router-dom';
import AuthPage from './components/pages/AuthPage';
import MainProductsPage from './components/pages/MainProductsPage';
import ProfilePage from './components/pages/ProfilePage';
import Footer from './components/shared/Footer';
import { IUser } from './types/types';
import { getUser } from './utils/authApiUtils';

export default function App() {
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);
  const [isAdmin, setIsAdmin] = useState<boolean>(false);
  const [currentUser, setCurrentUser] = useState<IUser | null>(null);
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const user = await getUser();
        if (user) {
          setIsLoggedIn(true);
          setIsAdmin(user.userRole.toLowerCase() === 'admin');
          setCurrentUser(user);
        }
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  if (loading) return <></>;

  return (
    <>
      <Routes>
        <Route
          path='/'
          element={
            <MainProductsPage isAdmin={isAdmin} currentUser={currentUser} />
          }
        />
        <Route
          path='/products'
          element={
            <MainProductsPage isAdmin={isAdmin} currentUser={currentUser} />
          }
        />
        <Route path='/login' element={<AuthPage />} />
        {isLoggedIn && (
          <Route
            path='/profile'
            element={
              <ProfilePage isAdmin={isAdmin} currentUser={currentUser} />
            }
          />
        )}
        {!isLoggedIn && <Route path='/profile' element={<AuthPage />} />}
      </Routes>
      <hr style={{ marginTop: '3rem' }} />
      <Footer />
    </>
  );
}
