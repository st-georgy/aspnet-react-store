import {
  AccountCircle,
  AdminPanelSettings,
  Logout,
  Search as SearchIcon,
  ShoppingCart as ShoppingCartIcon,
} from '@mui/icons-material';
import {
  AppBar,
  Avatar,
  Button,
  Divider,
  IconButton,
  Menu,
  MenuItem,
  Paper,
  Toolbar,
} from '@mui/material';
import { useState } from 'react';
import { useLocation } from 'react-router-dom';
import logoSvg from '../assets/logo.svg';
import { logout } from '../utils/authApiUtils';
import SearchDrawer from './MainProductsPage/SearchDrawer';

interface NavigationBarProps {
  isLoggedIn: boolean;
  isAdmin: boolean;
}

export default function NavigationBar({
  isLoggedIn,
  isAdmin,
}: NavigationBarProps) {
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
              <SearchIcon fontSize='large' />
            </IconButton>

            <IconButton color='inherit'>
              <ShoppingCartIcon fontSize='large' />
            </IconButton>

            {!isLoggedIn && (
              <Button
                variant='outlined'
                style={{ width: '169px', marginLeft: '20px' }}
                href='/login'
              >
                Войти
              </Button>
            )}
            {isLoggedIn && (
              <div>
                <IconButton
                  aria-label='account of current user'
                  aria-controls='menu-appbar'
                  aria-haspopup='true'
                  onClick={handleMenu}
                  color='inherit'
                >
                  <AccountCircle fontSize='large' />
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
                  <Paper
                    style={{
                      display: 'flex',
                      alignItems: 'center',
                      padding: '10px',
                    }}
                    elevation={0}
                  >
                    <Avatar />
                    <div className='userinfo'>
                      <span>&nbsp;&nbsp;Full Name</span>
                      <span className='menu-username'>
                        &nbsp;&nbsp;@username
                      </span>
                    </div>
                  </Paper>
                  <Divider sx={{ marginTop: '8px', marginBottom: '8px' }} />
                  <MenuItem onClick={handleClose}>
                    <AccountCircle />
                    &nbsp;&nbsp;Профиль
                  </MenuItem>
                  {isAdmin && (
                    <MenuItem onClick={handleClose}>
                      <AdminPanelSettings />
                      &nbsp;&nbsp;Панель управления
                    </MenuItem>
                  )}
                  <Divider sx={{ marginTop: '8px', marginBottom: '8px' }} />
                  <MenuItem onClick={handleLogout}>
                    <Logout />
                    &nbsp;&nbsp;Выйти
                  </MenuItem>
                </Menu>
              </div>
            )}
          </div>
        </Toolbar>
      </AppBar>
    </>
  );
}
