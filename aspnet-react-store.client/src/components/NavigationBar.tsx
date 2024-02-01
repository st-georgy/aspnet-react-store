import { useState } from 'react';
import { Button, Container, Nav, Navbar } from 'react-bootstrap';
import { CiSearch, CiShoppingCart } from 'react-icons/ci';
import { useLocation } from 'react-router-dom';
import logoSvg from '../assets/logo.svg';
import SearchOffcanvas from './ProductsPage/SearchOffcanvas';

export default function NavigationBar() {
  const location = useLocation();
  const searchParams = new URLSearchParams(location.search);
  const searchText = searchParams.get('searchText');

  const [showSearchInput, setShowSearchInput] = useState<boolean>(false);

  const handleSearchInputClose = () => setShowSearchInput(false);
  const handleSearchInputShow = () => setShowSearchInput(true);

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

          <SearchOffcanvas
            show={showSearchInput}
            handleClose={handleSearchInputClose}
            searchText={searchText ?? undefined}
          />

          <Nav>
            <Nav.Link onClick={handleSearchInputShow}>
              <CiSearch size={30} />
            </Nav.Link>
            <Nav.Link>
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
