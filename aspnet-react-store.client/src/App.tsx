import NavigationBar from "./components/NavigationBar";
import ProductsList from "./components/ProductsList";
import Footer from "./components/Footer";

export default function App() {

    return (
        <>
            <NavigationBar />
            <ProductsList />
            <hr className="mt-5" />
            <Footer />
        </>
    );

}