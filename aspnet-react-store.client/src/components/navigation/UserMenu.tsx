import { AccountCircle, AdminPanelSettings, Logout } from '@mui/icons-material';
import {
  Avatar,
  Divider,
  IconButton,
  Menu,
  MenuItem,
  Paper,
} from '@mui/material';
import { useState } from 'react';
import { logout } from '../../utils/authApiUtils';

interface UserMenuProps {
  isAdmin: boolean;
}

export default function UserMenu({ isAdmin }: UserMenuProps) {
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);

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
    <div>
      <IconButton onClick={handleMenu} color='inherit'>
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
            minWidth: '150px',
          }}
          elevation={0}
        >
          <Avatar />
          <div className='userinfo'>
            <span>&nbsp;&nbsp;Full Name</span>
            <span className='menu-username'>&nbsp;&nbsp;@username</span>
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
  );
}