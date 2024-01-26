import axios from "axios";
import { useEffect, useState } from "react";
import IProduct from "../models/IProduct";
import { Row, Container } from "react-bootstrap";
import Product from "./Product";

export default function ProductsList() {

    const [products, setProducts] = useState<IProduct[]>([]);

    useEffect(() => {

        axios('api/products')
            .then(response => setProducts(response.data))
            .catch(error => console.error('Error fetching products, ', error));
    }, []);

    const productsInRows = [];

    for (let i = 0; i < products.length; i += 4) {
        const rowProducts = products.slice(i, i + 4);
        productsInRows.push(rowProducts);
    }

    return (
        <Container style={{ marginTop: "6rem" }}>
            {productsInRows.map((rowProducts, rowIndex) => (
                <Row key={rowIndex}>
                    {rowProducts.map((product, index) => (
                        <Product product={product} index={rowIndex + index} key={product.id} />
                    ))}
                </Row>
            ))}
        </Container>
    )
}