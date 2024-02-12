import { Search, ShoppingCart } from '@mui/icons-material';
import { AppBar, Button, IconButton, Toolbar } from '@mui/material';
import { useState } from 'react';
import { useLocation } from 'react-router-dom';
import logoSvg from '../../assets/logo.svg';
import SearchDrawer from '../pages/MainProductsPage/SearchDrawer';
import UserMenu from './UserMenu';

interface NavBarProps {
  isLoggedIn: boolean;
  isAdmin: boolean;
}

export default function NavBar({ isLoggedIn, isAdmin }: NavBarProps) {
  const location = useLocation();
  const searchParams = new URLSearchParams(location.search);
  const searchText = searchParams.get('searchText');

  const [showSearchDrawer, setShowSearchDrawer] = useState<boolean>(false);

  const handleSearchDrawerClose = () => setShowSearchDrawer(false);
  const handleSearchDrawerShow = () => setShowSearchDrawer(true);

  return (
    <>
      <AppBar position='static' color='inherit' elevation={0}>
        <SearchDrawer
          show={showSearchDrawer}
          handleClose={handleSearchDrawerClose}
          searchText={searchText ?? ''}
        />

        <Toolbar>
          <img src={logoSvg} height='80' alt='Logo' />

          <div
            style={{
              marginLeft: 'auto',
              display: 'flex',
              alignItems: 'center',
            }}
          >
            <IconButton color='inherit' onClick={handleSearchDrawerShow}>
              <Search fontSize='large' />
            </IconButton>

            <IconButton color='inherit'>
              <ShoppingCart fontSize='large' />
            </IconButton>

            {isLoggedIn ? (
              <UserMenu isAdmin={isAdmin} />
            ) : (
              <Button
                variant='outlined'
                style={{ width: '169px', marginLeft: '20px' }}
                href='/login'
              >
                Войти
              </Button>
            )}
          </div>
        </Toolbar>
      </AppBar>
    </>
  );
}
