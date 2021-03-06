﻿using System;
using System.Collections;
using System.IO;

namespace Wipro.Support.JProperties
{
    /// <summary>
    /// Hold Java style properties as key-value pairs and allow them to be loaded from or
    /// saved to a ".properties" file. The file is stored with character set ISO-8859-1 which extends US-ASCII
    /// (the characters 0-127 are the same) and forms the first part of the Unicode character set.  Within the
    /// application <see cref="string"/> are Unicode - but all values outside the basic US-ASCII set are escaped.
    /// </summary>
    public class JavaPropertiesbits : Hashtable
    {
        /// <summary>
        /// A reference to an optional set of default properties - these values are returned
        /// if the value has not been loaded from a ".properties" file or set programatically.
        /// </summary>
        protected Hashtable Defaults;

        /// <summary>
        /// An empty constructor that doesn't set the defaults.
        /// </summary>
        public JavaPropertiesbits()
        {
        }

        /// <summary>
        /// Use this constructor to provide a set of default values.  The default values are kept separate
        /// to the ones in this instant.
        /// </summary>
        /// <param name="defaults">A Hashtable that holds a set of defafult key value pairs to
        /// return when the requested key has not been set.</param>
        public JavaPropertiesbits(Hashtable defaults)
        {
            this.Defaults = defaults;
        }

        /// <summary>
        /// Load Java Properties from a stream expecting the format as described in <see cref="RPP_TestRPP_TesParseExceptionader"/>.
        /// </summary>
        /// <param name="streamIn">An input stream to read properties from.</param>
        /// <exception cref="JavaPropertyReader">If the stream source is invalid.</exception>
        public void Load(Stream streamIn)
        {
            var reader = new JavaPropertyReader(this);
            reader.Parse(streamIn);
        }

        /// <summary>
        /// Store the contents of this collection of properties to the stream in the format
        /// used for Java ".properties" files using an instance of <see cref="RPP_Testing.RPPMasRPP_TJavaPropertyWriterpertyWriter"/>.
        /// The keys and values will be minimally escaped to ensure special characters are read back
        /// in properly.  Keys are not sorted.  The file will begin with a comment identifying the
        /// date - and an additional comment may be included.
        /// 
        /// 
        /// </summary>
        /// <param name="streamOut">An output stream to write the properties to.</param>
        /// <param name="comments">Optional additional comment to include at the head of the output.</param>
        public void Store(Stream streamOut, string comments)
        {
            var writer = new JavaPropertyWriter(this);
            writer.Write(streamOut, comments);
        }

        /// <summary>
        /// Get the value for the specified key value.  If the key is not found, then return the
        /// default value - and if still not found, return null.
        /// </summary>
        /// <param name="key">The key whose value should be returned.</param>
        /// <returns>The value corresponding to the key - or null if not found.</returns>
        public string GetProperty(string key)
        {
            var objectValue = this[key];
            if (objectValue != null)
            {
                return AsString(objectValue);
            }
            else if (Defaults != null)
            {
                return AsString(Defaults[key]);
            }

            return null;
        }

        /// <summary>
        /// Get the value for the specified key value.  If the key is not found, then return the
        /// default value - and if still not found, return <c>defaultValue</c>.
        /// </summary>
        /// <param name="key">The key whose value should be returned.</param>
        /// <param name="defaultValue">The default value if the key is not found.</param>
        /// <returns>The value corresponding to the key - or null if not found.</returns>
        public string GetProperty(string key, string defaultValue)
        {
            return GetProperty(key) ?? defaultValue;
        }

        /// <summary>
        /// Set the value for a property key.  The old value is returned - if any.
        /// </summary>
        /// <param name="key">The key whose value is to be set.</param>
        /// <param name="newValue">The new value off the key.</param>
        /// <returns>The original value of the key - as a string.</returns>
        public string SetProperty(string key, string newValue)
        {
            var oldValue = AsString(this[key]);
            this[key] = newValue;
            return oldValue;
        }

        /// <summary>
        /// Returns an enumerator of all the properties available in this instance - including the
        /// defaults.
        /// </summary>
        /// <returns>An enumarator for all of the keys including defaults.</returns>
        public IEnumerator PropertyNames()
        {
            Hashtable combined;
            if (Defaults != null)
            {
                combined = new Hashtable(Defaults);

                for (var e = this.Keys.GetEnumerator(); e.MoveNext(); )
                {
                    var key = AsString(e.Current);
                    combined.Add(key, this[key]);
                }
            }
            else
            {
                combined = new Hashtable(this);
            }

            return combined.Keys.GetEnumerator();
        }

        /// <summary>
        /// A utility method to safely convert an <c>Object</c> to a <c>string</c>.
        /// </summary>
        /// <param name="o">An Object or null to be returned as a string.</param>
        /// <returns>string value of the object - or null.</returns>
        private string AsString(Object o)
        {
            return o == null ? null : o.ToString();
        }

    }
}
