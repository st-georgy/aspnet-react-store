import { Button, Typography } from '@mui/material';

interface ProfileFieldProps {
  title: string;
  value?: string | null | undefined;
}

export default function ProfileField({ title, value }: ProfileFieldProps) {
  return (
    <div
      style={{
        display: 'flex',
        flexDirection: 'row',
        marginTop: '1rem',
      }}
    >
      <div style={{ display: 'flex', flexDirection: 'column' }}>
        <Typography variant='h6' fontWeight='400'>
          {title}
        </Typography>
        <Typography
          variant='body1'
          style={{ color: 'grey', marginBottom: '8px' }}
        >
          {value ?? 'Не указано'}
        </Typography>
      </div>
      <div
        style={{
          marginTop: '1rem',
          marginLeft: 'auto',
        }}
      >
        <Button variant='outlined' color='primary'>
          Редактировать
        </Button>
      </div>
    </div>
  );
}
