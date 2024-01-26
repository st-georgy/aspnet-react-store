import { animated, useTrail, config } from "react-spring";
import IProduct from "../models/IProduct";
import { Col } from "react-bootstrap";
import "./style/product.css";

export default function Product({ product, index }: { product: IProduct, index: number }) {

    const trail = useTrail(index + 1, {
        opacity: 1,
        y: 0,
        from: { opacity: 0, y: 20 },
        config: config.gentle
    });

    return (
        <Col key={product.id} className="mb-4">
            <animated.div style={trail[index]} className="text-center product-block">
                <div className="image-container">
                <img className="product-image" src='src/assets/products/tshirt.png' alt={product.name} width={250}/>
                </div>
                <p className="product-name mb-0">{product.name}</p>
                <p className="product-price" style={{color: "#555"}}><em>{"₽" + product.price + " руб."}</em></p>
            </animated.div>
        </Col>
    )
}