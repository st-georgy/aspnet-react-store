import { Add, Remove } from '@mui/icons-material';
import { IconButton, Stack, Typography } from '@mui/material';

interface QuantityControlsProps {
  quantity: number;
}

export default function QuantityControls({ quantity }: QuantityControlsProps) {
  return (
    <Stack direction='row' alignItems='center' spacing={1}>
      <IconButton color='inherit'>
        <Remove />
      </IconButton>
      <Typography variant='body1' fontWeight='500'>
        {quantity}
      </Typography>
      <IconButton color='inherit'>
        <Add />
      </IconButton>
    </Stack>
  );
}
