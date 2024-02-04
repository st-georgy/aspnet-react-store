import {
  Search as SearchIcon,
  ShoppingCart as ShoppingCartIcon,
} from '@mui/icons-material';
import { AppBar, Button, IconButton, Toolbar } from '@mui/material';
import { useState } from 'react';
import { useLocation } from 'react-router-dom';
import logoSvg from '../assets/logo.svg';
import SearchDrawer from './MainProductsPage/SearchDrawer';

export default function NavigationBar() {
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
              <SearchIcon fontSize='medium' />
            </IconButton>

            <IconButton color='inherit'>
              <ShoppingCartIcon fontSize='medium' />
            </IconButton>

            <Button
              variant='outlined'
              style={{ width: '169px', marginLeft: '20px' }}
            >
              Войти
            </Button>
          </div>
        </Toolbar>
      </AppBar>
    </>
  );
}
