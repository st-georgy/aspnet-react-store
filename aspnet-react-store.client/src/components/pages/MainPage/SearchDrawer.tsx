import { Close as CloseIcon } from '@mui/icons-material';
import { Drawer, IconButton } from '@mui/material';
import { useEffect, useState } from 'react';
import '../../style/drawer.css';
import SearchForm from './SearchForm';

interface SearchDrawerProps {
  show: boolean;
  handleClose: () => void;
  searchText?: string;
}

export default function SearchDrawer({
  show,
  handleClose,
  searchText,
}: SearchDrawerProps) {
  const [searchInputValue, setSearchInputValue] = useState('');

  const handleSearch = () => {
    if (searchInputValue && searchInputValue.trim() !== '') {
      window.location.href =
        '/products?searchText=' + encodeURIComponent(searchInputValue.trim());
    }
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearchInputValue(e.target.value);
  };

  const handleKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
    if (e.key === 'Enter') {
      e.preventDefault();
      handleSearch();
    }
  };

  useEffect(() => {
    if (searchText) setSearchInputValue(searchText);
  }, []);

  return (
    <Drawer
      anchor='top'
      open={show}
      onClose={handleClose}
      onKeyDown={handleKeyDown}
    >
      <div className='drawer'>
        <div className='drawer-header'>
          <h3>Поиск товаров</h3>
          <IconButton onClick={handleClose}>
            <CloseIcon color='inherit' />
          </IconButton>
        </div>
        <div className='drawer-form'>
          <SearchForm
            value={searchInputValue}
            onChange={handleInputChange}
            onClick={handleSearch}
          />
        </div>
      </div>
    </Drawer>
  );
}
