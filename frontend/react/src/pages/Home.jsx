import React from 'react';
import Announcement from '../components/Announcement';
import Navbar from '../components/Navbar';
import Slider from '../components/Slider';
import Categories from '../components/Categories';
import Products from '../components/Products';
import Newsletter from '../components/Newsletter';
import Footer from '../components/Footer';

const Home = () => (
  <div>
    <Announcement></Announcement>
    <Navbar></Navbar>
    <Slider></Slider>
    <Categories></Categories>
    <Products></Products>
    <Newsletter></Newsletter>
    <Footer></Footer>
  </div>
);

export default Home;
