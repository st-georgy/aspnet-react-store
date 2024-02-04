import { Route, Routes } from 'react-router-dom';
import Footer from './components/Footer';
import LoginPage from './components/pages/LoginPage';
import MainProductsPage from './components/pages/MainProductsPage';

export default function App() {
  return (
    <>
      <Routes>
        <Route path='/' element={<MainProductsPage />} />
        <Route path='/products' element={<MainProductsPage />} />
        <Route path='/login' element={<LoginPage />} />
      </Routes>
      <hr style={{ marginTop: '3rem' }} />
      <Footer />
    </>
  );
}
