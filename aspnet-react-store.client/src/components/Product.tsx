import { animated, useTrail, config } from "react-spring";
import { IProduct } from "../models/models";
import ProductImages from "./ProductImages";
import { Card, Col } from "react-bootstrap";
import "./style/product.css";

export default function Product({ product, index }: { product: IProduct, index: number }) {

    const trail = useTrail(index + 1, {
        opacity: 1,
        y: 0,
        from: { opacity: 0, y: 20 },
        config: config.gentle
    });

    return (
        <Col key={index} className="mb-4">
            <animated.div style={trail[index]} className="product-block d-flex justify-content-around">
                <Card style={{width: 250}} className="image-container">
                    <ProductImages />
                    <hr style={{margin: "0"}}/>
                    <Card.Body>
                        <Card.Title className="product-name">
                            {product?.name}
                        </Card.Title>
                        <Card.Text>
                            <em>{"₽" + product?.price +" руб."}</em>
                        </Card.Text>
                    </Card.Body>
                </Card>
            </animated.div>
        </Col>
    )
}