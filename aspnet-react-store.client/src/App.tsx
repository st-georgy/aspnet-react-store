import Footer from './components/Footer';
import NavigationBar from './components/NavigationBar';
import ProductsList from './components/ProductsList';

export default function App() {
  return (
    <>
      <NavigationBar />
      <ProductsList />
      <hr className='mt-5' />
      <Footer />
    </>
  );
}
