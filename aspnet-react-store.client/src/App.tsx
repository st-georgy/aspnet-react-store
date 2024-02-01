import { Route, Routes } from 'react-router-dom';
import Footer from './components/Footer';
import NavigationBar from './components/NavigationBar';
import ProductsPage from './components/pages/ProductsPage';

export default function App() {
  return (
    <>
      <NavigationBar />
      <Routes>
        <Route path='/' element={<ProductsPage />} />
        <Route path='/products' element={<ProductsPage />} />
      </Routes>
      <hr className='mt-5' />
      <Footer />
    </>
  );
}
