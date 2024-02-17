import { Button, Typography } from '@mui/material';
import { Link } from 'react-router-dom';
import logoSvg from '../../assets/logo.svg';

export default function NotFoundPage() {
  return (
    <div style={{ textAlign: 'center' }}>
      <a href='/'>
        <img src={logoSvg} height='120' alt='Logo' />
      </a>
      <div style={{ marginTop: '50px' }}>
        <Typography variant='h1' gutterBottom>
          404 - Not Found
        </Typography>
        <Typography variant='body1' gutterBottom mb={3}>
          Извините, но страницы, которую вы ищете, не существует
        </Typography>
        <Button component={Link} to='/' variant='outlined' color='primary'>
          На главную
        </Button>
      </div>
    </div>
  );
}
