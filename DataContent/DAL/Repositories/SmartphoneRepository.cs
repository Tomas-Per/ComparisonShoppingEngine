﻿using DataContent.DAL.Interfaces;
using DataManipulation.DataFillers;
using ModelLibrary;
using ModelLibrary.DataContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContent.DAL.Repositories
{
    public class SmartphoneRepository : ISmartphoneRepository
    {
        private readonly SmartphoneContext _context;

        public SmartphoneRepository(SmartphoneContext context)
        {
            _context = context;
        }
        public async Task<List<Smartphone>> AddSmartphonesAsync(List<Smartphone> smartphones)
        {
            foreach(var smartphone in smartphones)
            {
                //check for smartphone in DB
                var sameSmartphone = _context.Smartphones.Where(s => s.Name == smartphone.Name
                                                          && s.ShopName == smartphone.ShopName)
                                                         .FirstOrDefault();
                //change price and mofidy date if the smartphone is found in DB
                if (sameSmartphone != null)
                {
                    sameSmartphone.Price = smartphone.Price;
                    sameSmartphone.ModifyDate = smartphone.ModifyDate;
                }

                else
                {
                    smartphone.ItemCode = _context.Smartphones.Count();

                    //extract simiilar Smartphone from DB
                    var similarSmartphones = _context.Smartphones
                                        .Where(x => x.RAM == smartphone.RAM
                                        && x.Storage == smartphone.Storage
                                        && x.ScreenDiagonal == smartphone.ScreenDiagonal)
                                        .ToList();

                    //check and get list of equal Smartphones, but in other shops
                    var sameSmartphones = new List<Smartphone>();
                    foreach (var similarSmartphone in similarSmartphones)
                    {
                        if (similarSmartphone.Equals(smartphone)) sameSmartphones.Add(similarSmartphone);
                    }

                    //if equal Smartphones found, fill all possible information
                    if (sameSmartphones.Count() > 0)
                    {
                        sameSmartphone = new SmartphoneFiller().FillSmartphones(sameSmartphones);
                        foreach (var samePhone in sameSmartphones)
                        {
                            samePhone.ManufacturerName = sameSmartphone.ManufacturerName;
                            samePhone.BatteryStorage = sameSmartphone.BatteryStorage;
                            samePhone.Processor = samePhone.Processor;
                        }
                        smartphone.ItemCode = sameSmartphones[0].Id;
                    }

                    //update equal Smartphones in DB
                    await _context.SaveChangesAsync();

                    //add new Smartphone
                    _context.Add(smartphone);
                }
            }
            await _context.SaveChangesAsync();
            return smartphones;
        }

        public async Task<Smartphone> DeleteSmartphoneAsync(int id)
        {
            var smartphone = await _context.Smartphones.Where(x => x.Id == id).FirstOrDefaultAsync();
            _context.Smartphones.Remove(smartphone);
            await _context.SaveChangesAsync();
            return smartphone;
        }

        public async Task<List<Smartphone>> GetAllSmartphonesAsync()
        {
            var smartphones = await _context.Smartphones.ToListAsync();
            return smartphones;
        }

        public async Task<Smartphone> GetSmartphoneByIdAsync(int id)
        {
            var smartphone = await _context.Smartphones.Where(s => s.Id == id).FirstOrDefaultAsync();
            return smartphone;
        }

        public async Task<Smartphone> UpdateSmartphoneAsync(Smartphone smartphone)
        {
            var smartphoneInDb = await _context.Smartphones.Where(s => s.Id == smartphone.Id).FirstOrDefaultAsync();
            if (smartphoneInDb != null)
            {
                smartphoneInDb.Name = smartphone.Name;
                smartphoneInDb.ManufacturerName = smartphone.ManufacturerName;
                smartphoneInDb.Price = smartphone.Price;
                smartphoneInDb.ItemURL = smartphone.ItemURL;
                smartphoneInDb.ShopName = smartphone.ShopName;
                smartphoneInDb.ImageLink = smartphone.ImageLink;
                smartphoneInDb.ItemCategory = smartphone.ItemCategory;
                smartphoneInDb.Processor = smartphone.Processor;
                smartphoneInDb.FrontCameras = smartphone.FrontCameras;
                smartphoneInDb.BackCameras = smartphone.BackCameras;
                smartphoneInDb.ScreenDiagonal = smartphone.ScreenDiagonal;
                smartphoneInDb.Storage = smartphoneInDb.Storage;
                smartphoneInDb.RAM = smartphone.RAM;
                smartphoneInDb.Resolution = smartphone.Resolution;
                smartphoneInDb.BatteryStorage = smartphone.BatteryStorage;
                smartphoneInDb.ModifyDate = DateTime.Now;
            }
            await _context.SaveChangesAsync();
            return smartphoneInDb;
        }
    }
}
