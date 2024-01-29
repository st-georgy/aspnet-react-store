import { Button, Container, Nav, Navbar } from 'react-bootstrap';
import { CiSearch, CiShoppingCart } from 'react-icons/ci';
import logoSvg from '../assets/logo.svg';

export default function NavigationBar() {
  return (
    <>
      <Navbar bg='white'>
        <Container>
          <Navbar.Brand href='/'>
            <img
              src={logoSvg}
              height='80'
              className='d-inline-block align-top'
              alt='Logo'
            />
          </Navbar.Brand>

          <Nav>
            <Nav.Link href='/search'>
              <CiSearch size={30} />
            </Nav.Link>
            <Nav.Link href='/cart'>
              <CiShoppingCart size={30} />
            </Nav.Link>

            <Button
              variant='outline-dark'
              style={{ width: '169px', marginLeft: '20px' }}
            >
              Войти
            </Button>

            {/* <NavDropdown title={"Личный кабинет"} id="basic-nav-dropdown">
                        <NavDropdown.Item href="/account">Мой профиль</NavDropdown.Item>
                        <NavDropdown.Item href="/logout">Выйти</NavDropdown.Item>
                    </NavDropdown> */}
          </Nav>
        </Container>
      </Navbar>
    </>
  );
}
