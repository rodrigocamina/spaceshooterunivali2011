#region File Description
//-----------------------------------------------------------------------------
// Game1.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;
using XNAGameSpaceShotter.src.bean;

namespace StorageDemo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SaveGame
    {
        public SavePlayer player = new SavePlayer();
        IAsyncResult result;
        Object stateobj;
        bool GameSaveRequested = false;
        bool GameLoadRequested = false;
        GamePadState currentState;
        static SaveGame instance;
        StorageDevice device;

        [Serializable]
        public struct SavePlayer
        {
            public SavePlayerBean score1;
            public SavePlayerBean score2;
            public SavePlayerBean score3;
            public SavePlayerBean score4;
            public SavePlayerBean score5;
            public SavePlayerBean score6;
            public SavePlayerBean score7;
            public SavePlayerBean score8;
            public SavePlayerBean score9;
            public SavePlayerBean score10;
        }

        [Serializable]
        public struct SavePlayerBean
        {
            public int score;
            public String name;
        }
        

        private SaveGame()
        {
        }


        public static SaveGame getInstance()
        {
            if (instance == null)
            {
                return instance = new SaveGame();
            }
            else
            {
                return instance;
            }
        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void CallSave(SavePlayer player)
        {
            this.player = player;
            // Set the request flag
            if ((!Guide.IsVisible) && (GameSaveRequested == false))
            {
                GameSaveRequested = true;
                result = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);
            }
        
        }
        public void CallLoad()
        {
            // Set the request flag
            if ((!Guide.IsVisible) && (GameSaveRequested == false))
            {
                GameLoadRequested = true;
                result = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);
            }

        }

        public void ResetDevice() {
            if (!Guide.IsVisible)
            {
                // Reset the device
                device = null;
                stateobj = (Object)"GetDevice for Player One";
                StorageDevice.BeginShowSelector(PlayerIndex.One, this.GetDevice, stateobj);
            }
        
        }

        public void Update()
        {
            if ((GameSaveRequested) && (result.IsCompleted))
            {
                StorageDevice device = StorageDevice.EndShowSelector(result);
                if (device != null && device.IsConnected)
                {
                    DoSaveGame(device);
                }
                // Reset the request flag
                GameSaveRequested = false;
            }

            if ((GameLoadRequested) && (result.IsCompleted))
            {
                StorageDevice device = StorageDevice.EndShowSelector(result);
                if (device != null && device.IsConnected)
                {
                    DoLoadGame(device);
                }
                // Reset the request flag
                GameLoadRequested = false;
            }
        }

        void GetDevice(IAsyncResult result)
        {
            device = StorageDevice.EndShowSelector(result);
            if (device != null && device.IsConnected)
            {
                DoSaveGame(device);
                DoLoadGame(device);
            }
        }

        /// <summary>
        /// This method serializes a data object into
        /// the StorageContainer for this game.
        /// </summary>
        /// <param name="device"></param>
        private void DoSaveGame(StorageDevice device)
        {
            

            // Open a storage container.
            IAsyncResult result =
                device.BeginOpenContainer("StorageDemo", null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            StorageContainer container = device.EndOpenContainer(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();

            string filename = "savegame.sav";

            // Check to see whether the save exists.
            if (container.FileExists(filename))
                // Delete it so that we can create one fresh.
                container.DeleteFile(filename);

            // Create the file.
            Stream stream = container.CreateFile(filename);

            // Convert the object to XML data and put it in the stream.
            XmlSerializer serializer = new XmlSerializer(typeof(SavePlayer));
            serializer.Serialize(stream, player);

            // Close the file.
            stream.Close();

            // Dispose the container, to commit changes.
            container.Dispose();
        }
        /// <summary>
        /// This method loads a serialized data object
        /// from the StorageContainer for this game.
        /// </summary>
        /// <param name="device"></param>
        private void DoLoadGame(StorageDevice device)
        {
            // Open a storage container.
            IAsyncResult result =
                device.BeginOpenContainer("StorageDemo", null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            StorageContainer container = device.EndOpenContainer(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();

            string filename = "savegame.sav";

            // Check to see whether the save exists.
            if (!container.FileExists(filename))
            {
                // If not, dispose of the container and return.
                container.Dispose();
                return;
            }

            // Open the file.
            Stream stream = container.OpenFile(filename, FileMode.Open);

            // Read the data from the file.
            XmlSerializer serializer = new XmlSerializer(typeof(SavePlayer));
            player = (SavePlayer)serializer.Deserialize(stream);

            // Close the file.
            stream.Close();

            // Dispose the container.
            container.Dispose();

            //// Report the data to the console.
            //Debug.WriteLine("Name:     " + data.PlayerName);
            //Debug.WriteLine("Level:    " + data.Level.ToString());
            //Debug.WriteLine("Score:    " + data.Score.ToString());
            //Debug.WriteLine("Position: " + data.AvatarPosition.ToString());
        }
        /// <summary>
        /// This method creates a file called demobinary.sav and places
        /// it in the StorageContainer for this game.
        /// </summary>
        /// <param name="device"></param>
        
        //private static void DoCreate(StorageDevice device)
        //{
        //    // Open a storage container.
        //    IAsyncResult result =
        //        device.BeginOpenContainer("StorageDemo", null, null);

        //    // Wait for the WaitHandle to become signaled.
        //    result.AsyncWaitHandle.WaitOne();

        //    StorageContainer container = device.EndOpenContainer(result);

        //    // Close the wait handle.
        //    result.AsyncWaitHandle.Close();

        //    // Add the container path to our file name.
        //    string filename = "demobinary.sav";

        //    // Create a new file.
        //    if (!container.FileExists(filename))
        //    {
        //        Stream file = container.CreateFile(filename);
        //        file.Close();
        //    }
        //    // Dispose the container, to commit the data.
        //    container.Dispose();
        //}
        ///// <summary>
        ///// This method illustrates how to open a file. It presumes
        ///// that demobinary.sav has been created.
        ///// </summary>
        ///// <param name="device"></param>
        //private static void DoOpen(StorageDevice device)
        //{
        //    IAsyncResult result =
        //        device.BeginOpenContainer("StorageDemo", null, null);

        //    // Wait for the WaitHandle to become signaled.
        //    result.AsyncWaitHandle.WaitOne();

        //    StorageContainer container = device.EndOpenContainer(result);

        //    // Close the wait handle.
        //    result.AsyncWaitHandle.Close();

        //    // Add the container path to our file name.
        //    string filename = "demobinary.sav";

        //    Stream file = container.OpenFile(filename, FileMode.Open);
        //    file.Close();

        //    // Dispose the container.
        //    container.Dispose();
        //}
       
        ///// <summary>
        ///// This method deletes a file previously created by this demo.
        ///// </summary>
        ///// <param name="device"></param>
        //private static void DoDelete(StorageDevice device)
        //{
        //    IAsyncResult result =
        //        device.BeginOpenContainer("StorageDemo", null, null);

        //    // Wait for the WaitHandle to become signaled.
        //    result.AsyncWaitHandle.WaitOne();

        //    StorageContainer container = device.EndOpenContainer(result);

        //    // Close the wait handle.
        //    result.AsyncWaitHandle.Close();

        //    // Add the container path to our file name.
        //    string filename = "demobinary.sav";

        //    if (container.FileExists(filename))
        //    {
        //        container.DeleteFile(filename);
        //    }

        //    // Dispose the container, to commit the change.
        //    container.Dispose();
        //}
        ///// <summary>
        ///// This method opens a file using System.IO classes and the
        ///// TitleLocation property.  It presumes that a file named
        ///// ship.dds has been deployed alongside the game.
        ///// </summary>
        //private static void DoOpenFile()
        //{
        //    try
        //    {
        //        System.IO.Stream stream = TitleContainer.OpenStream("ship.dds");
        //        System.IO.StreamReader sreader = new System.IO.StreamReader(stream);
        //        // use StreamReader.ReadLine or other methods to read the file data

        //        Console.WriteLine("File Size: " + stream.Length);
        //        stream.Close();
        //    }
        //    catch (System.IO.FileNotFoundException)
        //    {
        //        // this will be thrown by OpenStream if gamedata.txt
        //        // doesn't exist in the title storage location
        //    }
        //}
    }

}
