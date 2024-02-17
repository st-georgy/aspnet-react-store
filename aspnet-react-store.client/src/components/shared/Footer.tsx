import { Container, Link, Typography } from '@mui/material';

export default function Footer() {
  return (
    <div className='footer'>
      <footer>
        <hr />
        <Container>
          <Typography variant='body2' mt={3}>
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
    </div>
  );
}
