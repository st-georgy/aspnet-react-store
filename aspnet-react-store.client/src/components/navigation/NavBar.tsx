import {
  FavoriteBorder,
  Search,
  ShoppingCartOutlined,
} from '@mui/icons-material';
import {
  AppBar,
  Button,
  IconButton,
  Link,
  Toolbar,
  Tooltip,
} from '@mui/material';
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
          <Link href='/'>
            <img src={logoSvg} height='80' alt='Logo' />
          </Link>

          <div
            style={{
              marginLeft: 'auto',
              display: 'flex',
              alignItems: 'center',
            }}
          >
            {showSearch && (
              <Tooltip title='Поиск товаров' placement='bottom' arrow>
                <IconButton color='inherit' onClick={handleSearchDrawerShow}>
                  <Search fontSize='large' />
                </IconButton>
              </Tooltip>
            )}

            {currentUser && (
              <>
                <Tooltip title='Корзина' placement='bottom' arrow>
                  <IconButton color='inherit' href='/cart'>
                    <ShoppingCartOutlined fontSize='large' />
                  </IconButton>
                </Tooltip>
                <Tooltip title='Избранное' placement='bottom' arrow>
                  <IconButton color='inherit' href='/favorites'>
                    <FavoriteBorder fontSize='large' />
                  </IconButton>
                </Tooltip>
                <UserMenu currentUser={currentUser} isAdmin={isAdmin} />
              </>
            )}

            {!currentUser && (
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
