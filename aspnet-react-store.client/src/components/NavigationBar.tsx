import {
  AccountCircle,
  Search as SearchIcon,
  ShoppingCart as ShoppingCartIcon,
} from '@mui/icons-material';
import {
  AppBar,
  Button,
  IconButton,
  Menu,
  MenuItem,
  Toolbar,
} from '@mui/material';
import { useState } from 'react';
import { useLocation } from 'react-router-dom';
import logoSvg from '../assets/logo.svg';
import { logout } from '../utils/authApiUtils';
import SearchDrawer from './MainProductsPage/SearchDrawer';

interface NavigationBarProps {
  loggedIn: boolean;
}

export default function NavigationBar({ loggedIn }: NavigationBarProps) {
  const location = useLocation();
  const searchParams = new URLSearchParams(location.search);
  const searchText = searchParams.get('searchText');

  const [showSearchDrawer, setShowSearchDrawer] = useState<boolean>(false);
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);

  const handleSearchDrawerClose = () => setShowSearchDrawer(false);
  const handleSearchDrawerShow = () => setShowSearchDrawer(true);

  const handleClose = () => {
    setAnchorEl(null);
  };

  const handleLogout = () => {
    logout().then((success) => {
      if (success) window.location.href = '/';
    });
  };

  const handleMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

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

            {!loggedIn && (
              <Button
                variant='outlined'
                style={{ width: '169px', marginLeft: '20px' }}
                href='/login'
              >
                Войти
              </Button>
            )}
            {loggedIn && (
              <div>
                <IconButton
                  size='large'
                  aria-label='account of current user'
                  aria-controls='menu-appbar'
                  aria-haspopup='true'
                  onClick={handleMenu}
                  color='inherit'
                >
                  <AccountCircle />
                </IconButton>
                <Menu
                  sx={{ mt: '45px' }}
                  id='menu-appbar'
                  anchorEl={anchorEl}
                  anchorOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                  }}
                  keepMounted
                  transformOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                  }}
                  open={Boolean(anchorEl)}
                  onClose={handleClose}
                >
                  <MenuItem onClick={handleClose}>Профиль</MenuItem>
                  <MenuItem onClick={handleLogout}>Выйти</MenuItem>
                </Menu>
              </div>
            )}
          </div>
        </Toolbar>
      </AppBar>
    </>
  );
}
