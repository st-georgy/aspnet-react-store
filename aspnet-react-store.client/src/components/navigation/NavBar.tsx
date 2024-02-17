import { Search, ShoppingCart } from '@mui/icons-material';
import { AppBar, Button, IconButton, Toolbar } from '@mui/material';
import { useState } from 'react';
import { useLocation } from 'react-router-dom';
import logoSvg from '../../assets/logo.svg';
import { IUser } from '../../types/types';
import SearchDrawer from '../pages/MainPage/SearchDrawer';
import UserMenu from './UserMenu';

interface NavBarProps {
  currentUser: IUser | null;
  isAdmin: boolean;
  showSearch: boolean;
}

export default function NavBar({
  currentUser,
  isAdmin,
  showSearch,
}: NavBarProps) {
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
          <a href='/'>
            <img src={logoSvg} height='80' alt='Logo' />
          </a>

          <div
            style={{
              marginLeft: 'auto',
              display: 'flex',
              alignItems: 'center',
            }}
          >
            {showSearch && (
              <IconButton color='inherit' onClick={handleSearchDrawerShow}>
                <Search fontSize='large' />
              </IconButton>
            )}

            <IconButton color='inherit'>
              <ShoppingCart fontSize='large' />
            </IconButton>

            {currentUser ? (
              <UserMenu currentUser={currentUser} isAdmin={isAdmin} />
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
