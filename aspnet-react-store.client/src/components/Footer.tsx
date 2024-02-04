import { Container, Link, Typography } from '@mui/material';

export default function Footer() {
  return (
    <footer style={{ padding: '16px 0', textAlign: 'center' }}>
      <Container>
        <Typography variant='body2'>
          &copy; 2024 asp-react-store by{' '}
          <Link
            href='https://github.com/st-georgy'
            target='_blank'
            rel='noopener noreferrer'
            underline='hover'
          >
            st-georgy
          </Link>
        </Typography>
      </Container>
    </footer>
  );
}
