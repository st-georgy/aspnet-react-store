import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';
import { useEffect, useState } from 'react';
import { Route, Routes } from 'react-router-dom';
import AuthPage from './components/pages/AuthPage';
import MainPage from './components/pages/MainPage';
import NotFoundPage from './components/pages/NotFoundPage';
import ProductPage from './components/pages/ProductPage';
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
      <div className='app'>
        <Routes>
          <Route
            path='/'
            element={<MainPage isAdmin={isAdmin} currentUser={currentUser} />}
          />
          <Route
            path='/products'
            element={<MainPage isAdmin={isAdmin} currentUser={currentUser} />}
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
          <Route
            path='/products/:id'
            element={
              <ProductPage currentUser={currentUser} isAdmin={isAdmin} />
            }
          />
          <Route path='*' element={<NotFoundPage />} />
        </Routes>
        <Footer />
      </div>
    </>
  );
}
