import { Nav, Navbar, NavDropdown, Container } from 'react-bootstrap';
import { CiShoppingCart, CiSearch } from 'react-icons/ci';

function App() {

    return (
        <Navbar bg="white" data-bs-theme="light">
            <Container>
                <Navbar.Brand href="/">asp-react-store</Navbar.Brand>
                <Nav className="me-auto">
                    <Nav.Link href="/">Главная</Nav.Link>
                    <Nav.Link href="/about">О нас</Nav.Link>
                </Nav>
                <Nav>
                    <Nav.Link href="/search">
                        <CiSearch size={30} />
                    </Nav.Link>
                    <Nav.Link href="/cart">
                        <CiShoppingCart size={30} />
                    </Nav.Link>
                    <NavDropdown title={"Личный кабинет"} id="basic-nav-dropdown">
                        <NavDropdown.Item href="/account">My Account</NavDropdown.Item>
                        <NavDropdown.Item href="/logout">Logout</NavDropdown.Item>
                    </NavDropdown>
                    
                </Nav>
            </Container>
        </Navbar>
    );

}

export default App;