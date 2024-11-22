﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MusicViewer
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="MusicManagerDB")]
	public partial class MusicDataClassesDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertAuthorMusic(AuthorMusic instance);
    partial void UpdateAuthorMusic(AuthorMusic instance);
    partial void DeleteAuthorMusic(AuthorMusic instance);
    partial void InsertAuthor(Author instance);
    partial void UpdateAuthor(Author instance);
    partial void DeleteAuthor(Author instance);
    partial void InsertMusicFile(MusicFile instance);
    partial void UpdateMusicFile(MusicFile instance);
    partial void DeleteMusicFile(MusicFile instance);
    partial void InsertMusic(Music instance);
    partial void UpdateMusic(Music instance);
    partial void DeleteMusic(Music instance);
    #endregion
		
		public MusicDataClassesDataContext() : 
				base(global::MusicViewer.Properties.Settings.Default.MusicManagerDBConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public MusicDataClassesDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MusicDataClassesDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MusicDataClassesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MusicDataClassesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<AuthorMusic> AuthorMusics
		{
			get
			{
				return this.GetTable<AuthorMusic>();
			}
		}
		
		public System.Data.Linq.Table<Author> Authors
		{
			get
			{
				return this.GetTable<Author>();
			}
		}
		
		public System.Data.Linq.Table<MusicFile> MusicFiles
		{
			get
			{
				return this.GetTable<MusicFile>();
			}
		}
		
		public System.Data.Linq.Table<Music> Musics
		{
			get
			{
				return this.GetTable<Music>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.AuthorMusic")]
	public partial class AuthorMusic : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private int _MusicId;
		
		private int _AuthorId;
		
		private EntityRef<Author> _Author;
		
		private EntityRef<Music> _Music;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnMusicIdChanging(int value);
    partial void OnMusicIdChanged();
    partial void OnAuthorIdChanging(int value);
    partial void OnAuthorIdChanged();
    #endregion
		
		public AuthorMusic()
		{
			this._Author = default(EntityRef<Author>);
			this._Music = default(EntityRef<Music>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MusicId", DbType="Int NOT NULL")]
		public int MusicId
		{
			get
			{
				return this._MusicId;
			}
			set
			{
				if ((this._MusicId != value))
				{
					if (this._Music.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnMusicIdChanging(value);
					this.SendPropertyChanging();
					this._MusicId = value;
					this.SendPropertyChanged("MusicId");
					this.OnMusicIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AuthorId", DbType="Int NOT NULL")]
		public int AuthorId
		{
			get
			{
				return this._AuthorId;
			}
			set
			{
				if ((this._AuthorId != value))
				{
					if (this._Author.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnAuthorIdChanging(value);
					this.SendPropertyChanging();
					this._AuthorId = value;
					this.SendPropertyChanged("AuthorId");
					this.OnAuthorIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Author_AuthorMusic", Storage="_Author", ThisKey="AuthorId", OtherKey="Id", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public Author Author
		{
			get
			{
				return this._Author.Entity;
			}
			set
			{
				Author previousValue = this._Author.Entity;
				if (((previousValue != value) 
							|| (this._Author.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Author.Entity = null;
						previousValue.AuthorMusics.Remove(this);
					}
					this._Author.Entity = value;
					if ((value != null))
					{
						value.AuthorMusics.Add(this);
						this._AuthorId = value.Id;
					}
					else
					{
						this._AuthorId = default(int);
					}
					this.SendPropertyChanged("Author");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Music_AuthorMusic", Storage="_Music", ThisKey="MusicId", OtherKey="Id", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public Music Music
		{
			get
			{
				return this._Music.Entity;
			}
			set
			{
				Music previousValue = this._Music.Entity;
				if (((previousValue != value) 
							|| (this._Music.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Music.Entity = null;
						previousValue.AuthorMusics.Remove(this);
					}
					this._Music.Entity = value;
					if ((value != null))
					{
						value.AuthorMusics.Add(this);
						this._MusicId = value.Id;
					}
					else
					{
						this._MusicId = default(int);
					}
					this.SendPropertyChanged("Music");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Author")]
	public partial class Author : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Name;
		
		private EntitySet<AuthorMusic> _AuthorMusics;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    #endregion
		
		public Author()
		{
			this._AuthorMusics = new EntitySet<AuthorMusic>(new Action<AuthorMusic>(this.attach_AuthorMusics), new Action<AuthorMusic>(this.detach_AuthorMusics));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Author_AuthorMusic", Storage="_AuthorMusics", ThisKey="Id", OtherKey="AuthorId")]
		public EntitySet<AuthorMusic> AuthorMusics
		{
			get
			{
				return this._AuthorMusics;
			}
			set
			{
				this._AuthorMusics.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_AuthorMusics(AuthorMusic entity)
		{
			this.SendPropertyChanging();
			entity.Author = this;
		}
		
		private void detach_AuthorMusics(AuthorMusic entity)
		{
			this.SendPropertyChanging();
			entity.Author = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Files")]
	public partial class MusicFile : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private System.Data.Linq.Link<byte[]> _FileMp3;
		
		private EntitySet<Music> _Musics;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void Onmp3Changing(byte[] value);
    partial void Onmp3Changed();
    #endregion
		
		public MusicFile()
		{
			this._Musics = new EntitySet<Music>(new Action<Music>(this.attach_Musics), new Action<Music>(this.detach_Musics));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="FileMp3", Storage="_FileMp3", DbType="VARBINARY (MAX) NULL", CanBeNull=false)]
		public byte[] mp3
		{
			get
			{
				return this._FileMp3.Value;
			}
			set
			{
				if ((this._FileMp3.Value != value))
				{
					this.Onmp3Changing(value);
					this.SendPropertyChanging();
					this._FileMp3.Value = value;
					this.SendPropertyChanged("mp3");
					this.Onmp3Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="MusicFile_Music", Storage="_Musics", ThisKey="Id", OtherKey="FileID")]
		public EntitySet<Music> Musics
		{
			get
			{
				return this._Musics;
			}
			set
			{
				this._Musics.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Musics(Music entity)
		{
			this.SendPropertyChanging();
			entity.MusicFile = this;
		}
		
		private void detach_Musics(Music entity)
		{
			this.SendPropertyChanging();
			entity.MusicFile = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Music")]
	public partial class Music : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Title;
		
		private string _Duration;
		
		private string _Style;
		
		private System.Nullable<int> _FileID;
		
		private EntitySet<AuthorMusic> _AuthorMusics;
		
		private EntityRef<MusicFile> _File;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnTitleChanging(string value);
    partial void OnTitleChanged();
    partial void OnDurationChanging(string value);
    partial void OnDurationChanged();
    partial void OnStyleChanging(string value);
    partial void OnStyleChanged();
    partial void OnFileIDChanging(System.Nullable<int> value);
    partial void OnFileIDChanged();
    #endregion
		
		public Music()
		{
			this._AuthorMusics = new EntitySet<AuthorMusic>(new Action<AuthorMusic>(this.attach_AuthorMusics), new Action<AuthorMusic>(this.detach_AuthorMusics));
			this._File = default(EntityRef<MusicFile>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Title", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				if ((this._Title != value))
				{
					this.OnTitleChanging(value);
					this.SendPropertyChanging();
					this._Title = value;
					this.SendPropertyChanged("Title");
					this.OnTitleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Duration", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Duration
		{
			get
			{
				return this._Duration;
			}
			set
			{
				if ((this._Duration != value))
				{
					this.OnDurationChanging(value);
					this.SendPropertyChanging();
					this._Duration = value;
					this.SendPropertyChanged("Duration");
					this.OnDurationChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Style", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Style
		{
			get
			{
				return this._Style;
			}
			set
			{
				if ((this._Style != value))
				{
					this.OnStyleChanging(value);
					this.SendPropertyChanging();
					this._Style = value;
					this.SendPropertyChanged("Style");
					this.OnStyleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FileID", DbType="Int")]
		public System.Nullable<int> FileID
		{
			get
			{
				return this._FileID;
			}
			set
			{
				if ((this._FileID != value))
				{
					if (this._File.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnFileIDChanging(value);
					this.SendPropertyChanging();
					this._FileID = value;
					this.SendPropertyChanged("FileID");
					this.OnFileIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Music_AuthorMusic", Storage="_AuthorMusics", ThisKey="Id", OtherKey="MusicId")]
		public EntitySet<AuthorMusic> AuthorMusics
		{
			get
			{
				return this._AuthorMusics;
			}
			set
			{
				this._AuthorMusics.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="MusicFile_Music", Storage="_File", ThisKey="FileID", OtherKey="Id", IsForeignKey=true, DeleteRule="CASCADE")]
		public MusicFile MusicFile
		{
			get
			{
				return this._File.Entity;
			}
			set
			{
				MusicFile previousValue = this._File.Entity;
				if (((previousValue != value) 
							|| (this._File.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._File.Entity = null;
						previousValue.Musics.Remove(this);
					}
					this._File.Entity = value;
					if ((value != null))
					{
						value.Musics.Add(this);
						this._FileID = value.Id;
					}
					else
					{
						this._FileID = default(Nullable<int>);
					}
					this.SendPropertyChanged("MusicFile");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_AuthorMusics(AuthorMusic entity)
		{
			this.SendPropertyChanging();
			entity.Music = this;
		}
		
		private void detach_AuthorMusics(AuthorMusic entity)
		{
			this.SendPropertyChanging();
			entity.Music = null;
		}
	}
}
#pragma warning restore 1591
