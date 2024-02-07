import { Container } from '@mui/material';
import { useEffect, useState } from 'react';
import logoSvg from '../../assets/logo.svg';
import { validateToken } from '../../utils/authApiUtils';
import LoginForm from '../LoginPage/LoginForm';
import Tabs from '../LoginPage/LoginPageTabs';
import RegisterForm from '../LoginPage/RegisterForm';

export default function LoginPage() {
  const [value, setValue] = useState<number>(0);

  const handleChange = (_: React.SyntheticEvent, newValue: number) => {
    setValue(newValue);
  };

  useEffect(() => {
    validateToken().then((isTokenValid) => {
      if (isTokenValid) window.location.href = '/?loginSuccess=true';
    });
  });

  return (
    <>
      <a href='/' style={{ position: 'absolute' }}>
        Вернуться на главную
      </a>
      <Container>
        <div style={{ textAlign: 'center' }}>
          <a href='/'>
            <img src={logoSvg} height='90' alt='Logo' />
          </a>
        </div>

        <Tabs value={value} onChange={handleChange} />

        {value === 0 && <LoginForm setActiveTab={setValue} />}
        {value === 1 && <RegisterForm setActiveTab={setValue} />}
      </Container>
    </>
  );
}
